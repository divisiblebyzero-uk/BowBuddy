using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BowBuddy.Model;
using BowBuddy.View;
using BowBuddy.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BowBuddy
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreSheetsListPage : ContentPage
    {
        public ScoreSheetsListPage()
        {
            InitializeComponent();
        }

        private List<ScoreSheet> GetScoreSheets()
        {
            return new ScoreSheet[] {
                new ScoreSheet
            {
                Gender = ScoreSheet.GenderMale,
                BowType = ScoreSheet.BowTypeRecurve,
                AgeGroup = ScoreSheet.AgeGroupAdult,
                Date = DateTime.Now,
                Dozens = new Dozen[]
                {
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    },
                    new Dozen
                    {
                        Ends = new End[]
                        {
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            },
                            new End
                            {
                                Scores = new string[] {"X","9","8","7","M","M"}
                            }
                        }.ToList()
                    }
                }.ToList(),
                RoundName = "Portsmouth"
            }
                }.ToList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var scoreSheets = GetScoreSheets();

            listView.ItemsSource = scoreSheets.OrderBy(s => s.Date).ToList();
        }

        async void OnScoreSheetAddedClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreSheetEntryHeaderPage
            {
                BindingContext = new ScoreSheetEntryHeaderViewModel()
            });
            
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ScoreSheetPage
                {
                    BindingContext = e.SelectedItem as ScoreSheet
                });
            }
        }
    }
}