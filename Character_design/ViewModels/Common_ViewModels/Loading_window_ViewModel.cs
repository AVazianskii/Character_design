using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace Character_design
{
    internal class Loading_window_ViewModel : BaseViewModel
    {
        /*
        private static Loading_window_ViewModel _instance;

        public static Loading_window_ViewModel GetInstance()
        {
            if (_instance == null) { _instance = new Loading_window_ViewModel(); }
            return _instance;
        }
        */
        private int progress;
        private bool flag;
        private Task progress_bar;
        private CancellationTokenSource cancelTokenSource;



        public int Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyChanged("Progress"); }
        }



        public void Start_progress_bar(CancellationToken tkn)
        {
            Progress = 0;
            progress_bar = new Task(() =>
            {
                while (true)
                {
                    if (tkn.IsCancellationRequested)
                    {
                        return;
                    }
                    
                    Progress = Progress + 1;
                    if (Progress > 100)
                    {
                        Progress = 0;
                    }
                }
            }, tkn);
        }
        public void Stop_progress_bar()
        {
            cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
        }



        public Loading_window_ViewModel()
        {
            cancelTokenSource = new CancellationTokenSource();
            Progress = 0;
        }
    }
}
