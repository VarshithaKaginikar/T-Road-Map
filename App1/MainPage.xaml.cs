﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void RaiseComplaintAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ComplaintDetails()));
        }

        private async void ViewComplaints(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ComplaintsList()));

        }
    }
}
