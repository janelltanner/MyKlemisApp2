using System;
using Xamarin.Essentials;

namespace MyKlemisApp.Services
{
    public static class Settings
    {
        const string AdminKey = "is_admin_key";
        public static bool IsAdmin
        {
            get => Preferences.Get(AdminKey, false);
            set
            {
                if (IsAdmin == value)
                    return;
                Preferences.Set(AdminKey, value);
                //OnPropertyChanged(nameof(IsAdmin));
            }
        }

        //private static void OnPropertyChanged(string v)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
