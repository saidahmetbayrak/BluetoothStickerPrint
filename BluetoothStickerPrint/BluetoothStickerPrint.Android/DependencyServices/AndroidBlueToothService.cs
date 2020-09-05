﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Bluetooth;
using BluetoothStickerPrint.DependecyServices;
using BluetoothStickerPrint.Droid.DependencyServices;
using Java.Util;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidBlueToothService))]
namespace BluetoothStickerPrint.Droid.DependencyServices
{
    public class AndroidBlueToothService : IBlueToothService
    {
        public IList<string> GetDeviceList()
        {
            using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
            {
                var btdevice = bluetoothAdapter?.BondedDevices.Select(i => i.Name).ToList();
                return btdevice;
            }
        }

        public async Task Print(string deviceName, string text)
        {
            using (BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter)
            {
                BluetoothDevice device = (from bd in bluetoothAdapter?.BondedDevices
                                          where bd?.Name == deviceName
                                          select bd).FirstOrDefault();

                try
                {

                    using (BluetoothSocket bluetoothSocket = device?.
                        CreateRfcommSocketToServiceRecord(
                        UUID.FromString("00001101-0000-1000-8000-00805F9B34FB")))

                    {
                        bluetoothSocket?.Connect();
                        //Console.WriteLine("Helloo " + bluetoothSocket.IsConnected);
                        byte[] buffer = Encoding.UTF8.GetBytes(text);
                        bluetoothSocket?.OutputStream.Write(buffer, 0, buffer.Length);
                        bluetoothSocket.Close();
                    }

                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
        }
    }
}