using BluetoothStickerPrint.DependecyServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;
using ZXing.Common;
using Image = Xamarin.Forms.Image;

namespace BluetoothStickerPrint.ViewModel
{
   public class PagePrintViewModel
    {
        private readonly IBlueToothService _blueToothService;

        

        private IList<string> _deviceList;
        public IList<string> DeviceList
        {
            get
            {
                if (_deviceList == null)
                    _deviceList = new ObservableCollection<string>();
                return _deviceList;

            }
            set
            {
                _deviceList = value;
            }
        }

        private string _printCustomerNo;
        public string PrintCustomerNo
        {
            get
            {
                return _printCustomerNo;
            }
            set
            {
                _printCustomerNo = value;
            }
        }

        private string _printCustomerName;
        public string PrintCustomerName
        {
            get
            {
                return _printCustomerName;
            }
            set
            {
                _printCustomerName = value;
            }
        }

        private string _printFileNo;
        public string PrintFileNo
        {
            get
            {
                return _printFileNo;
            }
            set
            {
                _printFileNo = value;
            }
        }

        private string _printProductNo;
        public string PrintProductNo
        {
            get
            {
                return _printProductNo;
            }
            set
            {
                _printProductNo = value;
            }
        }

        private string _printQuantity;
        public string PrintQuantity
        {
            get
            {
                return  _printQuantity;
            }
            set
            {
                _printQuantity = value;
            }
        }

        private string _printBarcode;
        public string PrintBarcode
        {
            get
            {
                return _printBarcode;
            }
            set
            {
                _printBarcode = value;
            }
        }
  

        private string _selectedDevice;
        public string SelectedDevice
        {
            get
            {
                return _selectedDevice;
            }
            set
            {
                _selectedDevice = value;
            }
        }

        public ICommand PrintCommand => new Command(async () =>
        {

            string printPage = PrintCustomerNo + "\n" + PrintCustomerName + "\n" + PrintFileNo + "\n" + PrintProductNo + "\t\t Miktar :" + PrintQuantity + "\n"  + "\t" + PrintBarcode + "\n\n\n" ;

            if (printPage.Length > 0)
            {
                await _blueToothService.Print(SelectedDevice, printPage);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Hata Mesajı", "Yazdırma metni Boş!!", "Cancel");
            }
        });

        public PagePrintViewModel()
        {
             _blueToothService = DependencyService.Get<IBlueToothService>();
            BindDeviceList();
        }

        void BindDeviceList()
        {
            var list = _blueToothService.GetDeviceList();
            DeviceList.Clear();
            foreach (var item in list)
                DeviceList.Add(item);
        }

        //void GenerateBarcode()
        //{
        //    var stream = _blueToothService.GenerateQrImage(PrintBarcode, 250, 125);
        //    Stream strm = new MemoryStream(stream);
        //    PrintImage.Source = ImageSource.FromStream(() => strm);
        //}
    }
}

