﻿using BluetoothStickerPrint.Pages;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BluetoothStickerPrint
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PagePrint();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
