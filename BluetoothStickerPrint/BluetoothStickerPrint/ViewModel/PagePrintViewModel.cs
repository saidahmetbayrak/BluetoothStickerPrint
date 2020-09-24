using BluetoothStickerPrint.DependecyServices;
using BluetoothStickerPrint.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;


[assembly: Xamarin.Forms.Dependency(typeof(PagePrintViewModel))]
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
            string printPage = @"\n\n\n\n
                                ^XA
                               ^MMT
                               ^PW775
                               ^LL0615
                               ^LS0
                                ^CFA,35
                                 ^FO50,100 ^FDMUSTERINO^FS
                                  ^CFA,30
                                   ^FO50,180 ^FDMUSTERIADI ^FS

                                    ^FO20,240 ^GB710,1,3 ^FS

                                       ^CFA,40
                                        ^FO100,270 ^FDBELGENO^FS
                                         ^CFA,55
                                          ^FO100,340 ^FDMALZEMENO ^FS
                                           ^FO100,440 ^FDMIKTAR: ADET ^FS

                                              ^BY3,2,150
                                               ^FO80,550 ^BC ^FDSEPET ^FS
                                                    ^CFA,55
                                                    ^XZ
                                                    \n\n\n";

            printPage = printPage.Replace("MUSTERINO", PrintCustomerNo);
            printPage = printPage.Replace("MUSTERIADI", PrintCustomerName);
            printPage = printPage.Replace("BELGENO", PrintFileNo);
            printPage = printPage.Replace("MALZEMENO", PrintProductNo);
            printPage = printPage.Replace("MIKTAR", PrintQuantity);
            printPage = printPage.Replace("SEPET", PrintBarcode);

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

    }
}

