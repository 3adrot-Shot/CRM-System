using System.Windows;

namespace Table
{
    class MVC
    {
        private readonly Window window;

        private int borderResize = 5;
        public int OutherMargin { get; set; } = 6;
        //Min size of the window
        public double MinHeight { get; set; } = 559;
        public double MinWidth { get; set; } = 815;
        public Thickness OutherMarginThickness
        {
            get
            {
                if (window.WindowState == WindowState.Maximized)
                    return new Thickness(0);
                return new Thickness(OutherMargin);
            }
        }

        /// <summary>
        /// Set Resize border thickness
        /// </summary>
        public int BorderResize { get { return borderResize + OutherMargin; } set { borderResize = value; } }
        public Thickness BorderResizeThickness { get { return new Thickness(BorderResize); } }

        public MVC(Window window)
        {
            this.window = window;
            //Fix windew resize Issue
            var resizer = new Code.WindowResizer(window, (int)MinWidth, (int)MinHeight);
        }
    }
}
