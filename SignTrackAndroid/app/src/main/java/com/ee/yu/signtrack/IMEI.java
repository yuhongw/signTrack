package com.ee.yu.signtrack;

import android.content.Context;
import android.telephony.TelephonyManager;

/**
 * Created by yu on 2017/4/23.
 */
public  class IMEI {
    public static String get_dev_id(Context ctx){
        //Getting the Object of TelephonyManager
        TelephonyManager tManager = (TelephonyManager)ctx.getSystemService(Context.TELEPHONY_SERVICE);
        //Getting IMEI Number of Devide
        String Imei=tManager.getDeviceId();
        return Imei;
    }

}
