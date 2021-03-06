package com.ee.yu.signtrack;

import android.content.Context;
import android.databinding.DataBindingUtil;
import android.os.RemoteException;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.telephony.TelephonyManager;
import android.view.View;
import android.webkit.JavascriptInterface;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.TextView;

import com.ee.yu.signtrack.databinding.ActivityMainBinding;

import org.altbeacon.beacon.Beacon;
import org.altbeacon.beacon.BeaconConsumer;
import org.altbeacon.beacon.BeaconManager;
import org.altbeacon.beacon.BeaconParser;
import org.altbeacon.beacon.MonitorNotifier;
import org.altbeacon.beacon.RangeNotifier;
import org.altbeacon.beacon.Region;
import org.joda.time.DateTime;
import org.joda.time.Interval;

import java.text.DecimalFormat;
import java.util.Calendar;
import java.util.Collection;
import java.util.Date;
import java.util.Timer;
import java.util.TimerTask;


public class MainActivity extends AppCompatActivity implements BeaconConsumer {


    /** 调试标识 */
    protected static final String TAG = "MonitoringActivity";
    /** 重新调整格式 */
    public static final String IBEACON_FORMAT = "m:2-3=0215,i:4-19,i:20-21,i:22-23,p:24-24";
    /** 设置兴趣UUID */
    public static final String FILTER_UUID = "E2C56DB5-DFFB-48D2-B060-D0F5A71096E0";

    private BeaconManager beaconManager;
    private DateTime lastFound;
    ActivityMainBinding b;

    final String ServURL = "http://125.222.244.19/yu/students/signin";
    final String CBeconId = "fda50693-a4e2-4fb1-afcf-c6eb07647825";

    @Override
    @JavascriptInterface
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        lastFound = DateTime.now().plusHours(-1);

        this.b = DataBindingUtil.setContentView(this, com.ee.yu.signtrack.R.layout.activity_main);
        beaconManager = BeaconManager.getInstanceForApplication(this);
        beaconManager.getBeaconParsers().add(new BeaconParser().setBeaconLayout(IBEACON_FORMAT));
        beaconManager.bind(this);

        final WebView webV =b.web;

        //设置定时器
        Timer timer = new Timer();
        timer.schedule(new TimerTask() {
            @Override
            public void run() {
                if (lastFound.isBefore(DateTime.now().minusMinutes(5)))
                {
                    String cmd = "javascript:setBeacon('0000-0000')";
                    b.web.loadUrl(cmd);
                }
            }
        },1000,1000);


        webV.getSettings().setJavaScriptEnabled(true);
        webV.setWebViewClient(new WebViewClient(){

            @Override
            public void onPageFinished(WebView view, String url) {
                super.onPageFinished(view, url);
                String cmd = "javascript:setPhone('" + IMEI.get_dev_id(MainActivity.this.getApplicationContext()) + "')";
                webV.loadUrl(cmd);
            }
        });


        Button btnGetInfo = b.btn1;
        btnGetInfo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                webV.loadUrl(ServURL);
            }
        });

        webV.loadUrl(ServURL);

    }

    @Override
    protected void onDestroy()
    {
        super.onDestroy();
        beaconManager.unbind(this);
    }

    @Override
    public void onBeaconServiceConnect()
    {
        beaconManager.setMonitorNotifier(new MonitorNotifier()
        {
            @Override
            public void didEnterRegion(Region region)
            {
                Log_e(TAG, "发现Beacon");
            }

            @Override
            public void didExitRegion(Region region)
            {
                Log_e(TAG, "Beacon消失");
            }

            @Override
            public void didDetermineStateForRegion(int state, Region region)
            {
                Log_e(TAG, "发现Beacon状态:" + state);
            }
        });

        beaconManager.setRangeNotifier(new RangeNotifier() {
            @Override
            public void didRangeBeaconsInRegion(Collection<Beacon> collection, Region region) {
                if (collection.size()>0)
                {
                    Beacon beacon = collection.iterator().next();
                    DecimalFormat df = new DecimalFormat("0.000");
                    double dist = beacon.getDistance()*10;
                    Log_e(TAG,"1st:"+ df.format(dist)+" m.");
                    if (beacon.getId1().toString().equals(CBeconId)) {
                        /*
                        if (dist > 0.1 && dist < 20) {
                            runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    b.web.setVisibility(View.VISIBLE);
                                }
                            });
                        } else {
                            runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    b.web.setVisibility(View.INVISIBLE);
                                }
                            });
                        }
                        */
                        lastFound = DateTime.now();
                        String cmd = "javascript:setBeacon('" + CBeconId + "')";
                        b.web.loadUrl(cmd);
                    }

                }
            }
        });

        try
        {
            Region region = new Region(FILTER_UUID, null, null, null);
            beaconManager.startMonitoringBeaconsInRegion(region);
            beaconManager.startRangingBeaconsInRegion(region);
        }
        catch (RemoteException e)
        {
            e.printStackTrace();
        }
    }

    private void Log_e(String tag,final String info)
    {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                b.txt2.setText(info);
            }
        });
    }

}
