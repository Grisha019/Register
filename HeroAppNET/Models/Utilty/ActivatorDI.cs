using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HeroAppNET.Models.Utilty
{
    public class ActivatorDI
    {
        private static ActivatorDI? Instance { get; set; } // Allow null values  

        private IServiceProvider _serviceProvider = null!; // Use null-forgiving operator to suppress CS8618  

        private ActivatorDI()
        {
        }

        public static ActivatorDI GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ActivatorDI();
            }

            return Instance;
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetService<T>() where T : class
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
