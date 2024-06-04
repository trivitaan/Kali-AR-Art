package com.kali.arapp;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Environment;
import android.provider.MediaStore;
import android.util.Log;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.channels.FileChannel;

public class NativeGallery {
    private static final String TAG = "NativeGallery";

    public static void saveImageToGallery(Context context, byte[] imageData, String filename) {
        try {
            File dir = new File(Environment.getExternalStoragePublicDirectory(Environment.DIRECTORY_DCIM), "Kali AR");
            if (!dir.exists()) {
                dir.mkdirs();
            }

            File file = new File(dir, filename);
            FileOutputStream fos = new FileOutputStream(file);
            fos.write(imageData);
            fos.close();

            // Update gallery
            MediaStore.Images.Media.insertImage(context.getContentResolver(), file.getAbsolutePath(), file.getName(), file.getName());

            Log.d(TAG, "Image saved to gallery");
        } catch (IOException e) {
            Log.e(TAG, "Error saving image to gallery: " + e.getMessage());
        }
    }
}
