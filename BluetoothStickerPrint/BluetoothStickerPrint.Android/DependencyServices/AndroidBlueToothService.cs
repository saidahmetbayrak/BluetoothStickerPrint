using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Bluetooth;
using Android.Graphics;
using BluetoothStickerPrint.DependecyServices;
using BluetoothStickerPrint.Droid.DependencyServices;
using Java.Util;
using Xamarin.Forms;
using ZXing;
using ZXing.QrCode;

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
                    if (device!=null)
                    {
                        using (BluetoothSocket bluetoothSocket = device?.
                       CreateRfcommSocketToServiceRecord(
                       UUID.FromString("00001101-0000-1000-8000-00805F9B34FB")))

                        {       
                                bluetoothSocket?.Connect();
                                byte[] buffer = Encoding.UTF8.GetBytes(text);
                                bluetoothSocket?.OutputStream.Write(buffer, 0, buffer.Length);
                                bluetoothSocket.Close();                                                     
                        }
                    }
                    else
                    {
                         await Application.Current.MainPage.DisplayAlert("Hata Mesajı", "Yazıcı seçimi boş geçilemez!!!", "Cancel");
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
            }
        }

        #region BarcodePrint
        //public byte[] GenerateQrImage(string content, int width, int height)
        //{
        //    var options = new QrCodeEncodingOptions
        //    {
        //        Height = height,
        //        Width = width,
        //        Margin = 0,
        //        PureBarcode = true
        //    };

        //    var writer = new BarcodeWriter
        //    {
        //        Format = BarcodeFormat.CODE_128,
        //        Options = options
        //    };


        //    var bitmap = writer.Write(content);
        //    if (bitmap != null)
        //    {

        //        using (var stream = new MemoryStream())
        //        {
        //            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
        //            return stream.ToArray();
        //        }
        //    }

        //    return null;
        //}
        #endregion
    }
}