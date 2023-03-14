using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel_generalas
{
    public partial class Form1 : Form
    {
        List<Flat> _flats;
        RealEstateEntities2 context = new RealEstateEntities2();
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            _flats = context.Flats.ToList();
        }

        
    }
}
