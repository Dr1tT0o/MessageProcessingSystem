using Napier_Message_Service.Commands;
using Napier_Message_Service.Models;
using Napier_Message_Service.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Napier_Message_Service.ViewModels
{
    public class ViewStatsViewModel : BaseViewModel
    {
        //Reference for UI window
        ViewStatsView view;

        //OBJECT USED TO PROCESS AND STORE MESSAGES
        MessageProcessingService MPS;

        //Text Blocks
        public string TrendingTextBlock { get; private set; }
        public string MentionsTextBlock { get; private set; }
        public string IncidentsTextBlock { get; private set; }

        //Commands
        public ICommand RefreshButtonCommand { get; private set; }

        public ViewStatsViewModel(ViewStatsView view)
        {
            this.view = view;

            MPS = MessageProcessingService.Instance;

            //Text Boxes
            TrendingTextBlock = "Trending";
            MentionsTextBlock = "Mentions";
            IncidentsTextBlock = "Incidents";


            //Commands
            RefreshButtonCommand = new RelayCommand(RefreshButtonClick);

            //Display trending list
            RefreshButtonClick();
        }

        public void DisplayTrendingList(ListView listView)
        {
            //Clear list view
            listView.Items.Clear();

            //Get trending list
            Dictionary<string, int> trending = MPS.GetTrendingList();

            if (trending != null && trending.Count > 0)
            {
                var trendingList = trending.ToList(); 
                trendingList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
                trendingList.Reverse();

                for (int i = 0; i < trendingList.Count; i++)
                {
                    listView.Items.Add(string.Format("{0, 4}. {1,-28}",(i+1), trendingList[i].Key+" (" + trendingList[i].Value+")"));
                }
            }
        }

        public void DisplayMentions(ListView listView)
        {
            //Clear list view
            listView.Items.Clear();

            List<string> mentions = MPS.GetListOfMentions();

            if (mentions != null && mentions.Count > 0)
            {
                foreach (string mention in mentions)
                {
                    listView.Items.Add(mention);
                }
            }
                
        }

        public void DisplayIncidents(ListView listView)
        {
            //Clear list view
            listView.Items.Clear();

            List<SIR> incidents = MPS.GetSirs();

            if (incidents != null && incidents.Count > 0)
            {
                foreach (SIR incident in incidents)
                {
                    listView.Items.Add(string.Format("{0,-8} {1, 24}", incident.SortCode, incident.NatureOfIncident));
                }
            }
        }


        public void RefreshButtonClick()
        {
            DisplayTrendingList(view.DisplayTrendingList);
            DisplayMentions(view.DisplayMentionsList);
            DisplayIncidents(view.DisplayIncidentsList);
        }
    }
}
