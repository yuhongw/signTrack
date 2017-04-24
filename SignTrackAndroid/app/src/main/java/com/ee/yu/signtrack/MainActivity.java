package com.ee.yu.signtrack;

import android.content.Context;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.telephony.TelephonyManager;
import android.view.View;
import android.webkit.JavascriptInterface;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.TextView;



public class MainActivity extends AppCompatActivity {

    final String ServURL = "http://125.222.244.19/yu/students/signin";
    @Override
    @JavascriptInterface
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        final WebView webV = (WebView) findViewById(R.id.webV);

        webV.getSettings().setJavaScriptEnabled(true);
        webV.setWebViewClient(new WebViewClient(){

            @Override
            public void onPageFinished(WebView view, String url) {
                super.onPageFinished(view, url);
                String cmd = "javascript:setPhone('" + IMEI.get_dev_id(MainActivity.this.getApplicationContext()) + "')";
                webV.loadUrl(cmd);
            }
        });

        webV.addJavascriptInterface(this,"sss");

        Button btnGetInfo = (Button) findViewById(R.id.btnGetInfo);
        btnGetInfo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                webV.loadUrl(ServURL);
            }
        });

        webV.loadUrl(ServURL);
    }

}
