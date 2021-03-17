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

        private string _printZPLFormmat;
        public string PrintZPLFormmat
        {
            get
            {
                return _printZPLFormmat;
            }
            set
            {
                _printZPLFormmat = value;
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
            string printPage = @"
                                ^XA
                                ^LT
                                ^MMT
                                ^PW775
                                ^LL0615
                                ^LS0
                                ^CI28
                                
                                ^CFA,30
                                ^FO30,20^FDMUSTERINO^FS
                                ^CFA,30
                                ^FO30,70^FDMUSTERIADI^FS
                            ^CFA,30
                                ^FO30,110^FDADRES^FS
                                ^FO20,150^GB500,1,3^FS
                                ^CFA,25
                                ^FO30,170^FDBELGENO /^FS
                                ^FO270,170^FDMIKTAR: ADET^FS
                                ^CFA,35
                                ^FO30,210^FDMALZEMENO^FS
                                ^BY3,2,100
                                ^FO70,250^BC^FDSEPET^FS
                                ^XZ
                                    ";
            //string printPage = PrintZPLFormmat;

            //printPage = printPage.Replace("MUSTERINO", PrintCustomerNo);
            //printPage = printPage.Replace("MUSTERIADI", PrintCustomerName);
            //printPage = printPage.Replace("BELGENO", PrintFileNo);
            //printPage = printPage.Replace("MALZEMENO", PrintProductNo);
            //printPage = printPage.Replace("MIKTAR", PrintQuantity);
            //printPage = printPage.Replace("SEPET", PrintBarcode);

            if (printPage.Length > 0)
            {
            //    string result = await Application.Current.MainPage.DisplayPromptAsync("Barkod Yazdırma", "Adet sayısı giriniz","Tamam",maxLength:2,keyboard:Keyboard.Numeric,initialValue:"1");
            //    int adet = int.Parse(result);
            //    for (int i = 0; i < adet; i++)
            //    {
                    await _blueToothService.Print(SelectedDevice, printPage);
               // }

                
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

