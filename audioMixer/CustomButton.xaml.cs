using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace audioMixer
{
    public partial class CustomButton : UserControl
    {

        private Ellipse ellipse;
        private Brush backgroundColor;
        private Brush contentColor;
        private string buttonType;


        public CustomButton()
        {
            InitializeComponent();
            this.ellipse = this.FindName("xEllipse") as Ellipse;
            this.backgroundColor = ellipse.Fill;
            this.contentColor = Brushes.White;

        }


        public Brush ButtonBackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                this.ellipse.Fill = value;
            }
        }


        public Brush ContentColor
        {
            get
            {
                return this.contentColor;
            }
            set
            {
                this.contentColor = value;
            }
        }


        public int Size
        {
            get
            {
                return (int)this.Height;
            }
            set
            {
                this.Width = value;
                this.Height = value;
                setElementsInside();
            }
        }


        public string ButtonType
        {
            get
            {
                return this.buttonType;
            }
            set
            {
                this.buttonType = value;
            }
        }


        private void setElementsInside()
        {
            PointCollection pointCollection;
            Polygon polygon;
            Rectangle rectangle;

            switch (buttonType)
            {
                case "Play":
                    polygon = new Polygon();
                    polygon.Fill = this.contentColor;
                    pointCollection = new PointCollection();
                    pointCollection.Add(new Point(this.Width / 3.5, this.Height / 4.2));
                    pointCollection.Add(new Point(this.Width / 3.5, this.Height - this.Height / 4.2));
                    pointCollection.Add(new Point((this.Width - this.Width / 4) + (this.Width / (this.Width / 2)), this.Height / 2));
                    polygon.Points = pointCollection;
                    this.xGrid.Children.Add(polygon);
                    break;

                case "Stop":
                    rectangle = new Rectangle();
                    rectangle.Fill = this.contentColor;
                    rectangle.Width = this.Width / 2.2;
                    rectangle.Height = this.Height / 2.2;
                    this.xGrid.Children.Add(rectangle);
                    break;

                case "Pause":
                    rectangle = new Rectangle();
                    rectangle.Fill = this.contentColor;
                    rectangle.Width = this.Width / 2.2;
                    rectangle.Height = this.Height / 2.2;
                    this.xGrid.Children.Add(rectangle);
                    rectangle = new Rectangle();
                    rectangle.Fill = this.backgroundColor;
                    rectangle.Width = this.Width / 10;
                    rectangle.Height = this.Height / 2.2;
                    this.xGrid.Children.Add(rectangle);
                    break;

                case "AddTrack":
                    rectangle = new Rectangle();
                    rectangle.Fill = this.contentColor;
                    rectangle.Width = this.Width / 8;
                    rectangle.Height = this.Height / 1.6;
                    this.xGrid.Children.Add(rectangle);
                    rectangle = new Rectangle();
                    rectangle.Fill = this.contentColor;
                    rectangle.Width = this.Width / 1.6;
                    rectangle.Height = this.Height / 8;
                    this.xGrid.Children.Add(rectangle);
                    break;

                default:
                    throw new Exception("Wrong button type. Set correct type.");
            }
        }

    }
}
