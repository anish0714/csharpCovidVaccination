using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml.Serialization;


namespace CovidVaccination
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ValidateScheduleForm();
        public delegate void ValidateEditForm();

        enum Vaccines {
            PFIZER, MODERNA, COVIDSHIELD
        }


        public ObservableCollection<Customer> CustList { get; set; } = null;
        public ObservableCollection<string> TimeSlotList { get; set; } =null;
        public Customer Customer { get; set; } = new SixtyPlus();
        string xmlFilePath = "vaccination_shedule.xml";

        
        bool isNameError = true;
        bool isAadharNumError = true;
        bool isUpdateNameErr = true;
        bool isUpdateAadharNumErr = true;
        string vaccine = string.Empty;


        ScheduledList listFromXmlObj = new ScheduledList();

        ICustomer custObj = new SixtyPlus(); 
        public MainWindow()
        {
            InitializeComponent();
            CustList = new ObservableCollection<Customer>();
            TimeSlotList = new ObservableCollection<string>();
            SetTimeslot();
            DataContext = this;
        }
        //------------------------------------------DELEGATES-------------------------------
        public void ValidateFormItem()
        {
            ValidateScheduleForm scheduleItem = SetNameTextBox;
            scheduleItem += SetAadharTextBox;
            scheduleItem();
        }
        public void ValidateUpdateFormItem()
        {
            ValidateEditForm updateItem = SetUpdateNameTextBox;
            updateItem += SetUpdateAadharTextBox;
            updateItem();
        }
        public void SetTimeslot()
        {
            TimeSlotList.Add("07:00 AM");
            TimeSlotList.Add("07:30 AM");
            TimeSlotList.Add("08:00 AM");
            TimeSlotList.Add("08:30 AM");
            TimeSlotList.Add("09:00 AM");
            TimeSlotList.Add("09:30 AM");
            TimeSlotList.Add("10:00 AM");
            TimeSlotList.Add("10:30 AM");
            TimeSlotList.Add("11:00 AM");
            TimeSlotList.Add("11:30 AM");
            GetXmlData();
            foreach (var schedule in listFromXmlObj)
            {
                RemoveTimeSlot(schedule.TimeSlot);
            }
            cbAppointment.ItemsSource = TimeSlotList;
            cbAppointment.SelectedIndex = 0;
        }
        public void RemoveTimeSlot(string time)
        {
            
            for (int i = 0; i < TimeSlotList.Count; i++)
            {
                Console.WriteLine(TimeSlotList[i]);

                if (TimeSlotList[i] == time)
                {
                    TimeSlotList.RemoveAt(i);
                }
            }
            
        }
   

        //==============================================METHOD====================================
        //---------------------------------------------NAME---------------------------------------
        public void SetNameTextBox()
        {
            if (custObj.ValidateName(tbCustName.Text))
            {
                tbCustName.Foreground = Brushes.Black;
                tbCustName.BorderBrush = new SolidColorBrush(Colors.Black);
                lNameErr.Content = "";
               isNameError = false;
            }
            else
            {
                tbCustName.BorderBrush = new SolidColorBrush(Colors.Red);
                tbCustName.Foreground = Brushes.Red;
                lNameErr.Content = "enter name in proper format";
                isNameError = true;
            }

        }
        public void SetUpdateNameTextBox()
        {
            if (custObj.ValidateName(tbUpdateName.Text))
            {
                tbUpdateName.Foreground = Brushes.Black;
                tbUpdateName.BorderBrush = new SolidColorBrush(Colors.Black);
                lEditNameErr.Content = "";
                isUpdateNameErr = false;
            }
            else
            {
                tbUpdateName.BorderBrush = new SolidColorBrush(Colors.Red);
                tbUpdateName.Foreground = Brushes.Red;
                lEditNameErr.Content = "enter name in proper format";
                isUpdateNameErr = true;
            }
        }

        //---------------------------------------------AGE GROUP--------------------------------------
        public void SetAgeGroup(RadioButton rb)
        {
            if (tbVaccine != null)
            {
                if (rb.Name == "rbSixty")
                {
                    tbVaccine.Text = GetPfizer();
                }
                else if (rb.Name == "rbFourtyFive")
                {
                    tbVaccine.Text = GetModerna();
                }
                else if (rb.Name == "rbEighteen")
                {
                    tbVaccine.Text = GetCovidShield();
                }
                vaccine = tbVaccine.Text;
            }  
        }

        //---------------------------------------------NAME---------------------------------------
        public void SetAadharTextBox()
        {
            if (custObj.ValidateAadharNumber(tbAadharNumber.Text))
            {
                tbAadharNumber.Foreground = Brushes.Black;
                tbAadharNumber.BorderBrush = new SolidColorBrush(Colors.Black);
                lAadharErr.Content = "";
                isAadharNumError = false;
            }
            else
            {
                tbAadharNumber.BorderBrush = new SolidColorBrush(Colors.Red);
                tbAadharNumber.Foreground = Brushes.Red;
                lAadharErr.Content = "please enter aadharcard in valid format";
                isAadharNumError = true;
            }

        }
        
        public void SetUpdateAadharTextBox()
        {
            if (custObj.ValidateAadharNumber(tbUpdateAadhar.Text))
            {
                tbUpdateAadhar.Foreground = Brushes.Black;
                tbUpdateAadhar.BorderBrush = new SolidColorBrush(Colors.Black);
                lEditAadharErr.Content = "";
                isUpdateAadharNumErr = false;
            }
            else
            {
                tbUpdateAadhar.BorderBrush = new SolidColorBrush(Colors.Red);
                tbUpdateAadhar.Foreground = Brushes.Red;
                lEditAadharErr.Content = "please enter aadharcard in valid format";
                isUpdateAadharNumErr = true;
            }

        }


        //-------------------------------------------SCHEDULE DATA------------------------------------
        public void GetScheduleData()
        {
            listFromXmlObj.ClearScheduleList();
            CustList.Clear();
            GetXmlData();
            foreach (var xmlData in listFromXmlObj)
            {
                CustList.Add(xmlData.Customer);
            }

            dgAppointmentList.ItemsSource = null;
            dgAppointmentList.ItemsSource = CustList;
        }
        //-------------------------------------------SCHEDULE BUTTON----------------------------------
       
        public void SchduleVaccination()
        {
            ValidateFormItem();
            if ((isNameError == true) || (isAadharNumError == true))
            {
                lSheduleAppointment.Foreground = Brushes.Red;
                lSheduleAppointment.Content = "Appointment not booked. Please fill all fields correctly.";
            }
            else
            {
                if(cbAppointment.SelectedIndex == -1)
                {
                    MessageBox.Show("All Appointments are booked.");
                }else
                {
                    listFromXmlObj.ClearScheduleList();
                    GetXmlData();
                    Schedule newSchedule = new Schedule();
                    newSchedule.Customer = CreateNewAppointment();
                    newSchedule.TimeSlot = cbAppointment.Text;
                    listFromXmlObj.Add(newSchedule);
                    WriteXmlData(listFromXmlObj);
                    string time = cbAppointment.Text;
                    cbAppointment.ItemsSource = null;
                    RemoveTimeSlot(time);
                    cbAppointment.ItemsSource = TimeSlotList;
                    cbAppointment.SelectedIndex = 0;
                    lSheduleAppointment.Foreground = Brushes.Black;
                    lSheduleAppointment.Content = "Appointment Booked.";

                }
            }
        }

        public Customer CreateNewAppointment()
        {
            Customer customer = null;
            string covidshield = GetCovidShield();
            string pfizer = GetPfizer();
            if (vaccine == covidshield)
            {
                customer = new EighteenToFourtyFive(tbCustName.Text, "Age 18 to 45", tbVaccine.Text, tbAadharNumber.Text, cbAppointment.Text);
            }
            else if (vaccine == pfizer)
            {
                customer = new SixtyPlus(tbCustName.Text, "Age 60 +", tbVaccine.Text, tbAadharNumber.Text, cbAppointment.Text);
            }
            else
            {
                customer = new FourtyFiveToSixty(tbCustName.Text, "Age 45 to 60", tbVaccine.Text, tbAadharNumber.Text, cbAppointment.Text);
            }
            return customer;
        }
        //------------------------------------------DELETE ROW---------------------------------------
        public void DeleteRow()
        {
            Customer selectedCust = (Customer)dgAppointmentList.SelectedItem;
            if (selectedCust != null)
            {
                int index = dgAppointmentList.SelectedIndex;
                listFromXmlObj.ClearScheduleList();
                GetXmlData();
                Schedule schLst = listFromXmlObj[index];
                listFromXmlObj.Remove(schLst);
                WriteXmlData(listFromXmlObj);
                GetScheduleData();
                TimeSlotList.Add(selectedCust.CustScheduleTime);
                lDeleteAppointment.Content = "";
            }
            else
            {
                lDeleteAppointment.Content = "Please select a row in Grid";
            }
            
        }
        

        //-----------------------------------------EDIT FORM--------------------------------------
        public void EditForm()
        {
            
            Customer selectedCust = (Customer)dgAppointmentList.SelectedItem;
            if (selectedCust != null)
            {
                EnableEditForm();
                DisableScheduleForm();
                int index = dgAppointmentList.SelectedIndex;
                listFromXmlObj.ClearScheduleList();
                GetXmlData();
                Schedule schLst = listFromXmlObj[index];
                tbUpdateName.Text = schLst.Customer.CustName;
                tbUpdateAadhar.Text = schLst.Customer.CustAadhar;
                lEditAppointment.Content = "";
            }
            else
            {
                lEditAppointment.Content = "Please select a row in Grid";
            }

        }
        //------------------------------------------UPDATE DATA-----------------------------------
        public void UpdateData()
        {
           
            ValidateUpdateFormItem();
            if ((isUpdateNameErr == true) || (isUpdateAadharNumErr == true))
            {
                lUpdateAppointments.Content = "Please enter all fields correctly";
            }
            else
            {
                int index = dgAppointmentList.SelectedIndex;
                listFromXmlObj.ClearScheduleList();
                GetXmlData();
                Schedule schLst = listFromXmlObj[index];
                foreach (var xmlData in listFromXmlObj)
                {
                    if (schLst.TimeSlot == xmlData.TimeSlot)
                    {
                        xmlData.Customer.CustName = tbUpdateName.Text;
                        xmlData.Customer.CustAadhar = tbUpdateAadhar.Text;
                    }
                }
                WriteXmlData(listFromXmlObj);
                DisableEditForm();
                EnableScheduleForm();
                GetScheduleData();
                lUpdateAppointments.Content = "";

            }
        }
        //----------------------------------------------------------------------------------------
        private void OnScheduleClick(object sender, RoutedEventArgs e)
        {
            SchduleVaccination();
        }
        //-------------------------------------------DISPLAY BUTTON----------------------------------
        private void OnDisplayClick(object sender, RoutedEventArgs e)
        {
            dgAppointmentList.ItemsSource = null;
            GetScheduleData();
        }

        private void WriteXmlData(ScheduledList myList)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(ScheduledList));
            TextWriter writer = new StreamWriter(xmlFilePath);
            serializer.Serialize(writer, myList);
            writer.Close();
        }

        private void GetXmlData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ScheduledList));
            if (File.Exists(xmlFilePath))
            {
                TextReader tr = new StreamReader(xmlFilePath);
                listFromXmlObj = (ScheduledList)serializer.Deserialize(tr);
                tr.Close();
            }
        }
        //============================================UI ELEMENTS METHODS=================================
        //---------------------------------------------NAME-------------------------------------
        private void NameTextBox(object sender, TextChangedEventArgs e)
        {
            SetNameTextBox();
        }
        //-------------------------------------------AGE GROUP----------------------------------
        private void AgeGroupRadioButton(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            SetAgeGroup(rb);
        }
        //-------------------------------------------VACCINE------------------------------------
        private void AadharTextBox(object sender, RoutedEventArgs e)
        {
            SetAadharTextBox();
        }

        private void OnDeleteClickButton(object sender, RoutedEventArgs e)
        {
            DeleteRow();
        }

        private void OnSearchClick(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text == null)
            {
                dgAppointmentList.ItemsSource = CustList;
            }
            else
            {
                var qryResult = from Customer in CustList
                                where Customer.CustVaccine == tbSearch.Text.ToUpper()
                                select Customer;
                dgAppointmentList.ItemsSource = qryResult;

            }
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            EditForm();
        }

        private void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateNameTextChange(object sender, TextChangedEventArgs e)
        {
            SetUpdateNameTextBox();
        }

        private void UpdateAadharTextChange(object sender, TextChangedEventArgs e)
        {
            SetUpdateAadharTextBox();
        }
        //---------------------------------------ENABLE ITEMS----------------------------------------
        public void EnableEditForm()
        {
            lEditForm.IsEnabled = true;
            lEditName.IsEnabled = true;
            tbUpdateName.IsEnabled = true;
            lEditAadharCard.IsEnabled = true;
            tbUpdateAadhar.IsEnabled = true;
            bUpdate.IsEnabled = true;
        }
        public void EnableScheduleForm()
        {
            lCustName.IsEnabled = true;
            tbCustName.IsEnabled = true;
            lAppointment.IsEnabled = true;
            cbAppointment.IsEnabled = true;
            lAgeGroup.IsEnabled = true;
            rbEighteen.IsEnabled = true;
            rbFourtyFive.IsEnabled = true;
            rbSixty.IsEnabled = true;
            lAadharNumber.IsEnabled = true;
            lAadharDesc.IsEnabled = true;
            tbAadharNumber.IsEnabled = true;
            bScheduleAppointment.IsEnabled = true;
            bDisplaySchedule.IsEnabled = true;
            bDeleteAppointment.IsEnabled = true;
            bEdit.IsEnabled = true;
            bSearch.IsEnabled = true;
            tbSearch.IsEnabled = true;
            dgAppointmentList.IsEnabled = true;
        }
        //---------------------------------------DISABLE ITEMS--------------------------------------
        public string GetCovidShield()
        {
            return Vaccines.COVIDSHIELD.ToString();
        }
        public string GetModerna()
        {
            return Vaccines.MODERNA.ToString();
        }
        public string GetPfizer()
        {
            return Vaccines.PFIZER.ToString();
        }
        public void DisableEditForm()
        {
            lEditForm.IsEnabled = false;
            lEditName.IsEnabled = false;
            tbUpdateName.IsEnabled = false;
            lEditAadharCard.IsEnabled = false;
            tbUpdateAadhar.IsEnabled = false;
            bUpdate.IsEnabled = false;
        }

        public void DisableScheduleForm()
        {
            lCustName.IsEnabled = false;
            tbCustName.IsEnabled = false;
            lAppointment.IsEnabled = false;
            cbAppointment.IsEnabled = false;
            lAgeGroup.IsEnabled = false;
            rbEighteen.IsEnabled = false;
            rbFourtyFive.IsEnabled = false;
            rbSixty.IsEnabled = false;
            lAadharNumber.IsEnabled = false;
            lAadharDesc.IsEnabled = false;
            tbAadharNumber.IsEnabled = false;
            bScheduleAppointment.IsEnabled = false;
            bDisplaySchedule.IsEnabled = false;
            bDeleteAppointment.IsEnabled = false;
            bEdit.IsEnabled = false;
            bSearch.IsEnabled = false;
            tbSearch.IsEnabled = false;
            dgAppointmentList.IsEnabled = false;
        }
    }
}
