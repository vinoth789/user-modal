using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace LFSmodel
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserParameters : Page
    {
        
        public int minimumTrackDistance;
        public int optimumTrackDistance;
        public int maximumTrackDistance;

        public int minimumTrackSlope;
        public int optimumTrackSlope;
        public int maximumTrackSlope;

        public int minimumSlopeAcrossTrack;
        public int optimumSlopeAcrossTrack;
        public int maximumSlopeAcrossTrack;


        public int directionValue;
        public int slopeValue;
        public int slopeAcrossValue;
        public float slopeChangeFrom;
        public float slopeChangeTo;
        public float deviation;

        List<ListItem> trackDistanceList;
        List<ListItem> slopeStandardTechniqueList;
        List<ListItem> slopeExtremeTechniqueList;
        List<ListItem> slopeAcrossStandardTechniqueList;
        List<ListItem> slopeAcrossExtremeTechniqueList;

        List<ListItem> windDirectionList;
        List<ListItem> trackDirectionList;



        public UserParameters()
        {
            InitializeComponent();
            this.DataContext = this;

            trackDistanceList = new List<ListItem>();
            trackDistanceList.Add(new ListItem() { Label = "Minimum distance (in meters) : ", Value = 20 });
            trackDistanceList.Add(new ListItem() { Label = "Optimum distance (in meters) : ", Value = 21 });
            trackDistanceList.Add(new ListItem() { Label = "Maximum distance (in meters) : ", Value = 25 });
            defaultDistanceValues.ItemsSource = trackDistanceList;

            slopeStandardTechniqueList = new List<ListItem>();
            slopeStandardTechniqueList.Add(new ListItem() { Label = "Minimum value (in %) : ", Value = -35 });
            slopeStandardTechniqueList.Add(new ListItem() { Label = "Optimum value (in %) : ", Value = 0 });
            slopeStandardTechniqueList.Add(new ListItem() { Label = "Maximum value (in %) : ", Value = 35 });
            SlopeStandardListBox.ItemsSource = slopeStandardTechniqueList;

            slopeExtremeTechniqueList = new List<ListItem>();
            slopeExtremeTechniqueList.Add(new ListItem() { Label = "Minimum value (in %) : ", Value = -60 });
            slopeExtremeTechniqueList.Add(new ListItem() { Label = "Optimum value (in %) : ", Value = 0 });
            slopeExtremeTechniqueList.Add(new ListItem() { Label = "Maximum value (in %) : ", Value = 60 });
            SlopeExtremeListBox.ItemsSource = slopeExtremeTechniqueList;

            slopeAcrossStandardTechniqueList = new List<ListItem>();
            slopeAcrossStandardTechniqueList.Add(new ListItem() { Label = "Minimum value (in %) : ", Value = -5 });
            slopeAcrossStandardTechniqueList.Add(new ListItem() { Label = "Optimum value (in %) : ", Value = 0 });
            slopeAcrossStandardTechniqueList.Add(new ListItem() { Label = "Maximum value (in %) : ", Value = 5 });
            SlopeAcrossStandardListBox.ItemsSource = slopeAcrossStandardTechniqueList;

            slopeAcrossExtremeTechniqueList = new List<ListItem>();
            slopeAcrossExtremeTechniqueList.Add(new ListItem() { Label = "Minimum value (in %) : ", Value = -7 });
            slopeAcrossExtremeTechniqueList.Add(new ListItem() { Label = "Optimum value (in %) : ", Value = 0 });
            slopeAcrossExtremeTechniqueList.Add(new ListItem() { Label = "Maximum value (in %) : ", Value = 7 });
            SlopeAcrossExtremeListBox.ItemsSource = slopeAcrossExtremeTechniqueList;


            windDirectionList = new List<ListItem>();
            windDirectionList.Add(new ListItem() { Label = "SouthWest - NorthEast", Value = 225 });
            windDirectionList.Add(new ListItem() { Label = "West - East", Value = 270 });
            windDirectionList.Add(new ListItem() { Label = "NorthWest - SouthEast", Value = 315 });
            windDirectionList.Add(new ListItem() { Label = "North - South", Value = 0 });
            windDirectionListBox.ItemsSource = windDirectionList;
            windDirectionListBox.SelectedIndex = 0;

            trackDirectionList = new List<ListItem>();
            trackDirectionList.Add(new ListItem() { Label = "NorthWest - SouthEast", Value = 225 });
            trackDirectionList.Add(new ListItem() { Label = "North - South", Value = 270 });
            trackDirectionList.Add(new ListItem() { Label = "NorthEast - SouthWest", Value = 315 });
            trackDirectionList.Add(new ListItem() { Label = "West - East", Value = 0 });
            trackDirectionListBox.ItemsSource = trackDirectionList;


        }



        private void Submit_Clicked(object sender, EventArgs e)
        {

            if (!(trackDistanceRadioBtnPanel.Children.OfType<RadioButton>().Any(rb => rb.IsChecked == true)))
            {
                MessageBox.Show("Please select any value for 'distance between the tracks'");
            }
            else if (!(trackDirectionRadioBtnPanel.Children.OfType<RadioButton>().Any(rb => rb.IsChecked == true)))
            {
                MessageBox.Show("Please select any value for 'Track and wind direction'");
            }
            else if (!(trackSlopeRadioBtnPanel.Children.OfType<RadioButton>().Any(rb => rb.IsChecked == true)))
            {
                MessageBox.Show("Please select any value for 'Slope of the tracks'");
            }
            else if (!(trackAcrossSlopeRadioBtnPanel.Children.OfType<RadioButton>().Any(rb => rb.IsChecked == true)))
            {
                MessageBox.Show("Please select any value for 'Slope across the tracks'");
            }
            else if (!(trackSlopeChangeRadioBtnPanel.Children.OfType<RadioButton>().Any(rb => rb.IsChecked == true)))
            {
                MessageBox.Show("Please select any value for 'Change in slope of the tracks'");
            }
            else if (!(trackCurveRadiusRadioBtnPanel.Children.OfType<RadioButton>().Any(rb => rb.IsChecked == true)))
            {
                MessageBox.Show("Please select any value for 'Curve radius of the tracks'");
            }
            else
            {

                if (customDistancePanel.IsVisible)
                {
                    if (!MinimumDistanceTextbox.Text.Equals("") && !OptimumDistanceTextbox.Text.Equals("") && !MaximumDistanceTextbox.Text.Equals(""))
                    {
                        int minimumDistanceValue = Int32.Parse(MinimumDistanceTextbox.Text);
                        int optimumDistanceValue = Int32.Parse(OptimumDistanceTextbox.Text);
                        int maximumDistanceValue = Int32.Parse(MaximumDistanceTextbox.Text);
                        if (minimumDistanceValue >= 20 && minimumDistanceValue < maximumDistanceValue && minimumDistanceValue < optimumDistanceValue)
                        {
                            minimumTrackDistance = minimumDistanceValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'minimum' field of 'distance between the tracks'");
                        }
                        if (optimumDistanceValue > minimumDistanceValue && optimumDistanceValue < maximumDistanceValue)
                        {
                            optimumTrackDistance = optimumDistanceValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'optimum' field of 'distance between the tracks'");
                        }
                        if (maximumDistanceValue > minimumDistanceValue && maximumDistanceValue > optimumDistanceValue)
                        {
                            maximumTrackDistance = maximumDistanceValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'maximum' field of 'distance between the tracks'");
                        }
                    }
                    else { MessageBox.Show("Values of 'distance between the tracks' cannot be empty"); }
                }

                if (SlopeOthersPanel.IsVisible)
                {
                    if (!MinimumSlopeTextbox.Text.Equals("") && !OptimumSlopeTextbox.Text.Equals("") && !MaximumSlopeTextbox.Text.Equals(""))
                    {
                        int minimumSlopeValue = Int32.Parse(MinimumSlopeTextbox.Text);
                        int optimumSlopeValue = Int32.Parse(OptimumSlopeTextbox.Text);
                        int maximumSlopeValue = Int32.Parse(MaximumSlopeTextbox.Text);
                        if (minimumSlopeValue > -70 && minimumSlopeValue < maximumSlopeValue && minimumSlopeValue < optimumSlopeValue)
                        {
                            minimumTrackSlope = minimumSlopeValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'minimum' field of 'slope of the tracks'");
                        }
                        if (optimumSlopeValue > minimumSlopeValue && optimumSlopeValue < maximumSlopeValue)
                        {
                            optimumTrackSlope = optimumSlopeValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'optimum' field of 'slope of the tracks'");
                        }
                        if (maximumSlopeValue > minimumSlopeValue && maximumSlopeValue > optimumSlopeValue)
                        {
                            maximumTrackSlope = maximumSlopeValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'maximum' field of 'slope of the tracks'");
                        }
                    }
                    else { MessageBox.Show("Values of 'slope of the tracks' cannot be empty"); }
                }

                if (SlopeAcrossOthersPanel.IsVisible)
                {
                    if (!MinimumSlopeAcrossTextbox.Text.Equals("") && !OptimumSlopeAcrossTextbox.Text.Equals("") && !MaximumSlopeAcrossTextbox.Text.Equals(""))
                    {
                        int minimumSlopeAcrossValue = Int32.Parse(MinimumSlopeAcrossTextbox.Text);
                        int optimumSlopeAcrossValue = Int32.Parse(OptimumSlopeAcrossTextbox.Text);
                        int maximumSlopeAcrossValue = Int32.Parse(MaximumSlopeAcrossTextbox.Text);
                        if (minimumSlopeAcrossValue > -7 && minimumSlopeAcrossValue < maximumSlopeAcrossValue && minimumSlopeAcrossValue < optimumSlopeAcrossValue)
                        {
                            minimumSlopeAcrossTrack = minimumSlopeAcrossValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'minimum' field of 'slope across the tracks'");
                        }
                        if (optimumSlopeAcrossValue > minimumSlopeAcrossValue && optimumSlopeAcrossValue < maximumSlopeAcrossValue)
                        {
                            optimumSlopeAcrossTrack = optimumSlopeAcrossValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'optimum' field of 'slope across the tracks'");
                        }
                        if (maximumSlopeAcrossValue > minimumSlopeAcrossValue && maximumSlopeAcrossValue > optimumSlopeAcrossValue)
                        {
                            maximumSlopeAcrossTrack = maximumSlopeAcrossValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'maximum' field of 'slope across the tracks'");
                        }
                    }
                    else { MessageBox.Show("Values of 'slope across the tracks' cannot be empty"); }
                }

                if (slopeChangeNormalpanel.IsVisible)
                {

                    float slopeChangeNormalFromValue = float.Parse(slopeChangeNormalFrom.Text, CultureInfo.InvariantCulture);
                    float slopeChangeNormalToValue = float.Parse(slopeChangeNormalTo.Text, CultureInfo.InvariantCulture);
                    slopeChangeFrom = slopeChangeNormalFromValue;
                    slopeChangeTo = slopeChangeNormalToValue;

                }
                else
                {
                    if (!slopeChangeOtherFromTextbox.Text.Equals("") && !slopeChangeOtherToTextbox.Text.Equals(""))
                    {
                        float slopeChangeOtherFromValue = float.Parse(slopeChangeOtherFromTextbox.Text, CultureInfo.InvariantCulture);
                        float slopeChangeOtherToValue = float.Parse(slopeChangeOtherToTextbox.Text, CultureInfo.InvariantCulture);

                        if (slopeChangeOtherFromValue > -15 && slopeChangeOtherFromValue < 14)
                        {
                            slopeChangeFrom = slopeChangeOtherFromValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'From' field of 'change in slope'");
                        }
                        if (slopeChangeOtherToValue > slopeChangeOtherFromValue && slopeChangeOtherToValue <= 15)
                        {
                            slopeChangeTo = slopeChangeOtherToValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'To' field of 'change in slope'");
                        }
                    }
                    else { MessageBox.Show("Values of 'change in slope' cannot be empty"); }
                }

                if (MaxDeviationTextBox.IsVisible)
                {
                    int maximumDeviationValue = Int32.Parse(MaximumDeviation.Text);
                    deviation = maximumDeviationValue;
                }
                else
                {
                    if (!otherDeviationTextBox.Text.Equals(""))
                    {
                        float otherDeviationValue = float.Parse(otherDeviationTextBox.Text, CultureInfo.InvariantCulture);

                        if (otherDeviationValue > 0 && otherDeviationValue <= 45)
                        {
                            deviation = otherDeviationValue;

                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid value for 'Curve radius of the tracks'");
                        }
                    }
                    else { MessageBox.Show("Value of 'Curve radius of the tracks' cannot be empty"); }
                }
            }
        }



        private void comboBox_Direction(object sender,
    System.Windows.Controls.SelectionChangedEventArgs e)
        {

            ComboBox cmb = (ComboBox)sender;
            directionValue = (int)cmb.SelectedValue;
            //selectedValue = ((ComboBoxItem)windDirectionListBox.SelectedValue).Content as string;      

        }




        private void Dist_Btn_Checked(object sender, RoutedEventArgs e)

        {

            if (DefaultDistance.IsChecked == true)
            {
                defaultDistancePanel.Visibility = Visibility.Visible;
                customDistancePanel.Visibility = Visibility.Collapsed;

                for (int i = 0; i < defaultDistanceValues.Items.Count - 2; ++i)
                {

                    dynamic minimumDistance = defaultDistanceValues.Items[0] as dynamic;
                    dynamic optimumDistance = defaultDistanceValues.Items[1] as dynamic;
                    dynamic maximumDistance = defaultDistanceValues.Items[2] as dynamic;

                    minimumTrackDistance = minimumDistance.Value;
                    optimumTrackDistance = optimumDistance.Value;
                    maximumTrackDistance = maximumDistance.Value;

                }

            }
            else if (CustomDistance.IsChecked == true)
            {
                defaultDistancePanel.Visibility = Visibility.Collapsed;
                customDistancePanel.Visibility = Visibility.Visible;

            }

        }

        private void Dir_Btn_Checked(object sender, RoutedEventArgs e)
        {
            if (WindDirecion.IsChecked == true)
            {
                windDirectionPanel.Visibility = Visibility.Visible;
                trackDirectionPanel.Visibility = Visibility.Collapsed;

            }
            else if (TrackDirection.IsChecked == true)
            {
                windDirectionPanel.Visibility = Visibility.Collapsed;
                trackDirectionPanel.Visibility = Visibility.Visible;

            }
            else if (NeglectDirection.IsChecked == true)
            {
                windDirectionPanel.Visibility = Visibility.Collapsed;
                trackDirectionPanel.Visibility = Visibility.Collapsed;

                directionValue = 1000;
            }

        }

        private void TextBox_slope_change(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.MaxLength = 5;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9.-]+");

        }
        private void TextBox_radius(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.MaxLength = 4;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9.]+");

        }

        private void TextBox_validation(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.MaxLength = 3;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9.-]+");
 
        }

        private void Slope_Btn_Checked(object sender, RoutedEventArgs e)
        {
            if (SlopeStandardTechnique.IsChecked == true)
            {
                SlopeStandardPanel.Visibility = Visibility.Visible;
                SlopeExtremePanel.Visibility = Visibility.Collapsed;
                SlopeOthersPanel.Visibility = Visibility.Collapsed;

                for (int i = 0; i < SlopeStandardListBox.Items.Count - 2; ++i)
                {

                    dynamic minimumSlope = SlopeStandardListBox.Items[0] as dynamic;
                    dynamic optimumSlope = SlopeStandardListBox.Items[1] as dynamic;
                    dynamic maximumSlope = SlopeStandardListBox.Items[2] as dynamic;

                    minimumTrackSlope = minimumSlope.Value;
                    optimumTrackSlope = optimumSlope.Value;
                    maximumTrackSlope = maximumSlope.Value;

                }

            }
            else if (SlopeExtremeTechnique.IsChecked == true)
            {
                SlopeStandardPanel.Visibility = Visibility.Collapsed;
                SlopeExtremePanel.Visibility = Visibility.Visible;
                SlopeOthersPanel.Visibility = Visibility.Collapsed;

                for (int i = 0; i < SlopeExtremeListBox.Items.Count - 2; ++i)
                {

                    dynamic minimumSlope = SlopeExtremeListBox.Items[0] as dynamic;
                    dynamic optimumSlope = SlopeExtremeListBox.Items[1] as dynamic;
                    dynamic maximumSlope = SlopeExtremeListBox.Items[2] as dynamic;

                    minimumTrackSlope = minimumSlope.Value;
                    optimumTrackSlope = optimumSlope.Value;
                    maximumTrackSlope = maximumSlope.Value;

                }

            }
            else if (SlopeOtherTechnique.IsChecked == true)
            {
                SlopeStandardPanel.Visibility = Visibility.Collapsed;
                SlopeExtremePanel.Visibility = Visibility.Collapsed;
                SlopeOthersPanel.Visibility = Visibility.Visible;
            }

        }

        private void Slope_Across_Btn_Checked(object sender, RoutedEventArgs e)
        {
            if (SlopeAcrossStandardTechnique.IsChecked == true)
            {
                SlopeAcrossStandardPanel.Visibility = Visibility.Visible;
                SlopeAcrossExtremePanel.Visibility = Visibility.Collapsed;
                SlopeAcrossOthersPanel.Visibility = Visibility.Collapsed;

                for (int i = 0; i < SlopeAcrossStandardListBox.Items.Count - 2; ++i)
                {

                    dynamic minimumSlopeAcross = SlopeAcrossStandardListBox.Items[0] as dynamic;
                    dynamic optimumSlopeAcross = SlopeAcrossStandardListBox.Items[1] as dynamic;
                    dynamic maximumSlopeAcross = SlopeAcrossStandardListBox.Items[2] as dynamic;

                    minimumSlopeAcrossTrack = minimumSlopeAcross.Value;
                    optimumSlopeAcrossTrack = optimumSlopeAcross.Value;
                    maximumSlopeAcrossTrack = maximumSlopeAcross.Value;

                }

            }
            else if (SlopeAcrossExtremeTechnique.IsChecked == true)
            {
                SlopeAcrossStandardPanel.Visibility = Visibility.Collapsed;
                SlopeAcrossExtremePanel.Visibility = Visibility.Visible;
                SlopeAcrossOthersPanel.Visibility = Visibility.Collapsed;

                for (int i = 0; i < SlopeAcrossExtremeListBox.Items.Count - 2; ++i)
                {

                    dynamic minimumSlopeAcross = SlopeAcrossExtremeListBox.Items[0] as dynamic;
                    dynamic optimumSlopeAcross = SlopeAcrossExtremeListBox.Items[1] as dynamic;
                    dynamic maximumSlopeAcross = SlopeAcrossExtremeListBox.Items[2] as dynamic;

                    minimumSlopeAcrossTrack = minimumSlopeAcross.Value;
                    optimumSlopeAcrossTrack = optimumSlopeAcross.Value;
                    maximumSlopeAcrossTrack = maximumSlopeAcross.Value;

                }
            }
            else if (SlopeAcrossOtherTechnique.IsChecked == true)
            {
                SlopeAcrossStandardPanel.Visibility = Visibility.Collapsed;
                SlopeAcrossExtremePanel.Visibility = Visibility.Collapsed;
                SlopeAcrossOthersPanel.Visibility = Visibility.Visible;
            }

        }
        private void Slope_Change_Btn_Checked(object sender, RoutedEventArgs e)
        {
            if (SlopeChangeDefault.IsChecked == true)
            {
                slopeChangeNormalpanel.Visibility = Visibility.Visible;
                slopeChangeOthersPanel.Visibility = Visibility.Collapsed;

            }
            else if (SlopeChangeOthers.IsChecked == true)
            {
                slopeChangeNormalpanel.Visibility = Visibility.Collapsed;
                slopeChangeOthersPanel.Visibility = Visibility.Visible;

            }

        }
        private void Curve_Radius_Btn_Checked(object sender, RoutedEventArgs e)
        {
            if (MaximumDeviationRadio.IsChecked == true)
            {
                MaxDeviationTextBox.Visibility = Visibility.Visible;
                otherDeviationPanel.Visibility = Visibility.Collapsed;

            }
            else if (Others.IsChecked == true)
            {
                MaxDeviationTextBox.Visibility = Visibility.Collapsed;
                otherDeviationPanel.Visibility = Visibility.Visible;

            }

        }
    }

    public class ListItem
    {
        public string Label { get; set; }
        public int Value { get; set; }


    }
    public class NameValidator : ValidationRule
    {
        private int min;
        private int opt;
        private int max;
        private String name;

        private static float minimumValue;
        private static float optimumValue = 0;
        private static float maximumValue = 0;

        private static float minimumSlope;
        private static float optimumSlope = 0;
        private static float maximumSlope = 0;

        private static float minimumSlopeAcross;
        private static float optimumSlopeAcross = 0;
        private static float maximumSlopeAcross = 0;

        private static float slopeChangeFrom;
        private static float slopeChangeTo;

        private static float curveRadius;



        public NameValidator()
        {
           
        }

        UserParameters _param;
        public NameValidator(UserParameters param) {
            _param = param;
        }
        //public string GetOuterString() { return o_.s; }


        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        public int Opt
        {
            get { return opt; }
            set { opt = value; }
        }
        public int Max
        {
            get { return max; }
            set { max = value; }
        }
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            float passedValue = 0;
            
            //int minimumSlopeValue = Int32.Parse(MinimumDistanceTextbox.ToString());
            //int optimumSlopeValue = Int32.Parse(OptimumSlopeTextbox.Text);
            //int maximumSlopeValue = Int32.Parse(MaximumSlopeTextbox.Text);
            if (!value.Equals(""))
                passedValue = float.Parse(value.ToString(), CultureInfo.InvariantCulture);


            if (name.Equals("MinimumDistance"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue < Min))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than 19");
                }
                else
                {
                    minimumValue = passedValue;
                    //_param.minimumTrackDistance = passedValue;
                }
            }
            
            if (name.Equals("OptimumDistance"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue <= minimumValue))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than the minimum value");
                }
                else if ((passedValue >= Max - 1))
                {
                    return new ValidationResult(false,
                      "Please enter a value less than 98");
                }
                else
                {
                    optimumValue = passedValue;
                    //_param.optimumTrackDistance = passedValue;
                }

            }
           
            if (name.Equals("MaximumDistance"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue < minimumValue))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than the minimum value");
                }
                else if ((passedValue < optimumValue))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than the optimum value");
                }
                else
                {
                    maximumValue = passedValue;
                    //_param.maximumTrackDistance = passedValue;
                }
            }


            if (name.Equals("MinimumSlope"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue < Min))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than -70");
                }
                else
                {
                    minimumSlope = passedValue;
                   // _param.minimumTrackSlope = passedValue;
                }
            }
            if (name.Equals("OptimumSlope"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue <= minimumSlope))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than minimum slope value");
                }
                else if ((passedValue >= Max))
                {
                    return new ValidationResult(false,
                      "Please enter a value lesser than 70");
                }
                else
                {
                    optimumSlope = passedValue;
                    //_param.optimumTrackSlope = passedValue;
                }
            }
            if (name.Equals("MaximumSlope"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue < minimumSlope))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than the minimum value");
                }
                else if ((passedValue < optimumSlope))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than the minimum value");
                }
                else if ((passedValue > Max))
                {
                    return new ValidationResult(false,
                      "Please enter a value lesser than 71");
                }
                else
                {
                    maximumSlope = passedValue;
                    //_param.maximumTrackSlope = passedValue;
                }
            }


            if (name.Equals("MinimumSlopeAcross"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue < Min))
                {
                    
                    return new ValidationResult(false,
                      "Please enter a value greater than -7");
                }
                else if ((passedValue > Max))
                {

                    return new ValidationResult(false,
                      "Please enter a value less than 7");
                }
                else
                {
                    minimumSlopeAcross = passedValue;
                   // _param.minimumSlopeAcrossTrack = passedValue;
                }
            }
            if (name.Equals("OptimumSlopeAcross"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue < minimumSlopeAcross))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than minimum value");
                }
                else if ((passedValue > Max))
                {

                    return new ValidationResult(false,
                      "Please enter a value less than 7");
                }
                else
                {
                    optimumSlopeAcross = passedValue;
                    //_param.optimumSlopeAcrossTrack = passedValue;
                }
            }
            if (name.Equals("MaximumSlopeAcross"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if ((passedValue < minimumSlopeAcross))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than the minimum value");
                }
                else if ((passedValue < optimumSlopeAcross))
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than the optimum value");
                }
                else if ((passedValue > Max))
                {

                    return new ValidationResult(false,
                      "Please enter a value less than 7");
                }
                else
                {
                    maximumSlopeAcross = passedValue;
                    //_param.maximumSlopeAcrossTrack = passedValue;
                }

            }


            if (name.Equals("slopeChangeOtherFrom"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if (passedValue <= Min)
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than -15");
                }
                else if (passedValue > Max-1)
                {
                    return new ValidationResult(false,
                      "Please enter a value lesser than 14");
                }
                else
                {
                    slopeChangeFrom = passedValue;
                    //_param.slopeChangeFrom = passedValue;
                }
            }
            if (name.Equals("slopeChangeOtherTo"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if (passedValue <= slopeChangeFrom)
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than from value");
                }
                else if (passedValue > Max)
                {
                    return new ValidationResult(false,
                      "Please enter a value lesser than 15");
                }
                else
                {
                    slopeChangeTo = passedValue;
                    //_param.slopeChangeTo = passedValue;
                }
            }


            if (name.Equals("OtherDeviation"))
            {
                if (value.Equals(""))
                    return new ValidationResult(false, "value cannot be empty.");
                else if (passedValue < Min)
                {
                    return new ValidationResult(false,
                      "Please enter a value greater than or equal to 0");
                }
                else if (passedValue > Max)
                {
                    return new ValidationResult(false,
                      "Please enter a value lesser than 45");
                }
                else
                {
                    curveRadius = passedValue;
                    //_param.deviation = passedValue;
                }
            }


            return ValidationResult.ValidResult;
        }
    }

}
