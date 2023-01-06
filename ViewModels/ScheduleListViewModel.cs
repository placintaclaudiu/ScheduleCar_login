using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScheduleCar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleCar.ViewModels
{
    public partial class ScheduleListViewModel : ObservableObject
    {
        public ObservableCollection<DaysModel> WeekDays { get; set; } = new ObservableCollection<DaysModel>();
        public ObservableCollection<ScheduleModel> ScheduleList { get; set; } = new ObservableCollection<ScheduleModel>();
        private List<ScheduleModel> _allScheduleList = new List<ScheduleModel>();

        [ObservableProperty]
        private DateTime _currentDate = DateTime.Now;

        [ObservableProperty]
        private bool _isBusy;

        public ScheduleListViewModel()
        {
            AddAllScheduleList();
        }

        private void AddAllScheduleList()
        {
            var scheudleList = new List<ScheduleModel>();
            scheudleList.Add(new ScheduleModel
            {
                CarName = "Dacia Logan",
                Price = "Pret: 500 lei", 
                Description = "An 2015 Motor 1.2 benzina Euro 6 unic, distribuție Schimbată la 90000, ulei +filtre schimbate recent",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(5),
                Location = "Locatie: Oradea",
                BackgroundColor = Color.FromArgb("#c0ccc0"),
            });

            scheudleList.Add(new ScheduleModel
            {
                CarName = "VolksWagen Polo 1.2 TDI 85CV",
                Price = "Pret: 700 lei",
                Description = "AN - 12/2013, motorizare - 1199,00cc 55kw, in dotare CÂRLIG de remorcat omologat!!! MAȘINA ESTE ADUSĂ RECENT IN ȚARA PE ROTI",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(5),
                Location = "Locatie: Zalau",
                BackgroundColor = Color.FromArgb("#332730"),
            });

            scheudleList.Add(new ScheduleModel
            {
                CarName = "Jeep Grand Cherokee , 2.7 Diesel",
                Price = "Pret: 1000 lei",
                Description = "2004, 137900 km, 2.7 diesel, 163 CP, cutie automata QuadraDrive, LSD fata spate și blocant central, încălzire in scaune.",
                StartDateTime = DateTime.Now.Date.AddDays(1).Add(new TimeSpan(13, 0, 0)),
                EndDateTime = DateTime.Now.Date.AddDays(1).Add(new TimeSpan(14, 0, 0, 0)).AddHours(4),
                Location = "Locatie: Vatra Dornei",
                BackgroundColor = Color.FromArgb("#56301d"),
            });

            scheudleList.Add(new ScheduleModel
            {
                CarName = "Land Rover Range Rover Sport 3.0 TDV6",
                Price = "Pret: 1200 lei",
                Description = "Motorizare - 2.0 180 CP Euro 6 HSE AdBlue- Cutie de viteze automata 9+1 trepte/padele- Tractiune integrala 4X4",
                StartDateTime = DateTime.Now.AddDays(1).Add(new TimeSpan(15, 0, 0)),
                EndDateTime = DateTime.Now.AddDays(1).Add(new TimeSpan(16, 0, 0)),
                Location = "Locatie: Suceava",
                BackgroundColor = Color.FromArgb("#9a6ead"),
            });

            scheudleList.Add(new ScheduleModel
            {
                CarName = "FORD TRANSIT MIXT 6 Locuri + Marfa/2.2 TDCI/2009",
                Price = "Pret: 600 lei",
                Description = "Autoutilitara 6 Locuri , Cutie de viteze manuală, Comenzi la Volan,Volan din piele",
                StartDateTime = DateTime.Now.AddDays(1).Add(new TimeSpan(17, 0, 0)),
                EndDateTime = DateTime.Now.AddDays(1).Add(new TimeSpan(18, 0, 0)),
                Location = "Locatie: Carlibaba",
                BackgroundColor = Color.FromArgb("#de999e"),
            });
           
            scheudleList.Add(new ScheduleModel
            {
                CarName = "Bmw X1 Xdrive 2.3D 4x4 Euro 5 PANO NAVI Xenon Automata Piele Camera",
                Price = "Pret: 1500 lei",
                Description = "Motor 2 Litri, Keyless GO/ ENTRY ( masina porneste si se deschide cu cheia in buzunar, t4x4 permanent",
                StartDateTime = DateTime.Now.AddDays(2).Add(new TimeSpan(19, 0, 0)),
                EndDateTime = DateTime.Now.AddDays(2).Add(new TimeSpan(20, 0, 0)),
                Location = "Locatie: Cluj",
                BackgroundColor = Color.FromArgb("#855b5e"),
            });

            scheudleList.Add(new ScheduleModel
            {
                CarName = "Renault Megane 2012",
                Price = "Pret: 750 lei",
                Description = "Benzina, Mp3/Mufa Aux/ Mufa USB, Pilot Automat, Jante aliaj R16 Pirelli Dot 21 noi",
                StartDateTime = DateTime.Now.AddDays(2).Add(new TimeSpan(19, 0, 0)),
                EndDateTime = DateTime.Now.AddDays(2).Add(new TimeSpan(20, 0, 0)),
                Location = "Locatie: Cluj",
                BackgroundColor = Color.FromArgb("#53504d"),
            });

            _allScheduleList.AddRange(scheudleList);

            BindDataToScheduleList();
        }


        public void BindDataToScheduleList()
        {

            IsBusy = true;
            Task.Run(async () =>
            {

                await Task.Delay(500);
                var filterScheduleList = _allScheduleList.Where(schedule => schedule.StartDateTime.Date == CurrentDate.Date).ToList();


                App.Current.Dispatcher.Dispatch(() =>
                {
                    ScheduleList.Clear();
                    foreach (var schedule in filterScheduleList)
                    {
                        ScheduleList.Add(schedule);
                    }
                    GetWeekDaysInfo();
                    IsBusy = false;
                });
            });


          
        }

        private void GetWeekDaysInfo()
        {
            DateTime startDayOfWeek = CurrentDate.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
                (int)CurrentDate.DayOfWeek);

            WeekDays.Clear();
            for(int i = 0; i < 7; i++)
            {
                var recordToAdd = new DaysModel
                {
                    DayName = DayOfWeekChar((int)startDayOfWeek.DayOfWeek),
                    Date = startDayOfWeek.Date,
                    IsSelected = startDayOfWeek.Date==CurrentDate.Date,
                };

                WeekDays.Add(recordToAdd);
                startDayOfWeek = startDayOfWeek.AddDays(1);
            }

        }

        private char DayOfWeekChar(int dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case 0:
                    return 'D';
                case 1:
                    return 'L';
                case 2:
                    return 'M';
                case 3:
                    return 'M';
                case 4:
                    return 'J';
                case 5:
                    return 'V';
                case 6:
                    return 'S';
            }
            return ' ';
        }


        [RelayCommand]
        public void WeekDaysSelected(DaysModel item)
        {
            WeekDays.ToList().ForEach(f => f.IsSelected = false);
            item.IsSelected = true;
            CurrentDate = item.Date;

            BindDataToScheduleList();
        }
    }
}
