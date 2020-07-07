using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowBuddy.Model;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BowBuddy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreSheetEntryPage : ContentPage
    {
        public ScoreSheetEntryPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Bow Buddy", JsonConvert.SerializeObject(BindingContext), "OK");
        }

        private void AddEndClicked(object sender, EventArgs e)
        {
            ((ScoreSheetEntryEndsViewModel) BindingContext).AddEnd();
        }
    }
    public class ScoreCircleTextColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (String)value;

            switch (stringValue)
            {
                case null:
                    return Color.Black;
                case "5":
                case "4":
                case "3":
                case "2":
                    return Color.White;
                default:
                    return Color.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not implemented.");
        }
    }

    public class ScoreCircleBackgroundColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = (String)value;

            switch (stringValue)
            {
                case null:
                    return Color.Orange;
                case "X":
                case "10":
                case "9":
                case "8":
                    return Color.Gold;
                case "7":
                case "6":
                    return Color.Red;
                case "5":
                case "4":
                    return Color.Blue;
                case "3":
                case "2":
                    return Color.Black;
                case "1":
                    return Color.White;
                case "M":
                    return Color.Green;
                default:
                    return Color.Orange;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Not implemented.");
        }
    }

}