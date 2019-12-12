using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wineapp.Models.ViewModels
{
    public class AdminVM
    {
        //Gráfico de los vinos mas visitados por la actividad de los usuarios
        public string[] colourNames { get; set; }
        public double[] colourScores { get; set; }

        //gráfico igual pero con las D.O
        public string[] sourceNames { get; set; }
        public double[] sourceScores { get; set; }

        //Top 10 global de los vinos mejor valorados
        public List<Wine> topWines { get; set; }

        //Top 5 de la semana de los vinos mejor valorados
        public List<string> winesName { get; set; }
        public Dictionary<string, int> winesScore { get; set; }


        //Prueba
        public string[] tintoNames { get; set; }
        public string[] blancoNames { get; set; }

    }
}
