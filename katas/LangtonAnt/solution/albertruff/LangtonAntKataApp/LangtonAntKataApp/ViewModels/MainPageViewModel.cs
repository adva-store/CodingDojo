using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangtonAntKataApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private int moveSpeed;
        public int MoveSpeed
        {
            set { SetProperty(ref moveSpeed, value); }
            get { return moveSpeed; }
        }

        public ICommand NavigateToFieldPageCommand { get; private set; }

        public MainPageViewModel()
        {
            MoveSpeed = 4;
            NavigateToFieldPageCommand = new Command(execute: async () =>
            {
                await Shell.Current.GoToAsync($"langtonAntsFieldPage?movespeed={MoveSpeed}");
            });
        }
    }
}
