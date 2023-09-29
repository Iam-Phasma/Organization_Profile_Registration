using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace OrganizationProfile
{
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        
        /////return methods 
        public long StudentNumber(string studNum)
        {
            try
            {
                if (studNum == "0" || studNum == null)
                {
                    throw new ArgumentNullException();
                    throw new FormatException();
                    throw new OverflowException();
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    _StudentNo = long.Parse(studNum);
                }
                
                
            }
            catch (FormatException snf)   //CHALLENGE 15 MULTIPLE CATCH THROWING FORMAT,NULL,OVERFLOW, AND OUTOFRANGE EXCEPTION
            {
                MessageBox.Show(" Student number is empty or contains invalid input! \n Error: FormatException");
            }catch (ArgumentNullException snan)
            {
                MessageBox.Show(" Student number is empty! \n Error: ArgumentNullException.");
            }catch(OverflowException snoe)
            {
                MessageBox.Show(" Student Number: Verification failed! \n Error: Overflowexception.");
            }catch (IndexOutOfRangeException snior)
            {
                MessageBox.Show(" Index out of range error sample. \n Error: IndexOutOfRangeException");
            }
            return _StudentNo;
        }

        public long ContactNo(string Contact)
        {
            try            /*@"^[0-9]{10-11}$"*/
            {
            if (Regex.IsMatch(Contact, @"^[09][0-9]{10}$")) //filter if the first 2 num starts with 09, the rest contains number ranging from 0-9, last is if the inputed number has 11 digits
            //if (Regex.IsMatch(Contact, "^[0-9]([0-9]*)$"))
                {
                _ContactNo = long.Parse(Contact);
                }
            }catch (Exception ecn)
            {
                //MessageBox.Show(ecn.Message);
            }
            
            return _ContactNo;
        }

        public string FullName(string LastName, string FirstName, string MiddleInitial)
        {
            if (Regex.IsMatch(LastName, @"^[a-zA-Z]+$") || Regex.IsMatch(FirstName, @"^[a-zA-Z]+$") || Regex.IsMatch(MiddleInitial, @"^[a-zA-Z]+$"))
            {
                _FullName = LastName + ", " + FirstName + ", " + MiddleInitial;
            }
            return _FullName;
        }

        public int Age(string age)
        {
            try
            {
                if (Regex.IsMatch(age, @"^[0-9]{1,3}$") && age != "0")
                {
                    _Age = Int32.Parse(age);
                }
                else
                {
                    throw new FormatException();  //CHALENGE 14 ELSE AND THROW EXCEPTION MANUALLY
                }
            }catch (FormatException ea)
            {
                //MessageBox.Show("     Age field is empty or contains invalid input! ");
                MessageBox.Show("Manually thrown Exception: " + ea.Message + "\nError: Either the age field is empty, has a value of 0, or grater than 150!");
            }
            return _Age;
        }
    



        private string _FullName;
        private int _Age;
        private long _ContactNo;
        private long _StudentNo;

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]
            {
                //"Unspecified",
                "BS Information Technology",
                "BS Computer Science",
                "BS Information Systems",
                "BS in Accountancy",
                "BS in Hospitality Management",
                "BS in Tourism Management"
            };
            for (int i = 0; i < 6; i++)
            {
                cbPrograms.Items.Add(ListOfProgram[i].ToString());
            }

            string[] ListOfGender = new string[]
            {
                //"Unspecified",
                "Male",
                "Female"
            };
            for (int i = 0; i < 2; i++)
            {
                cbGender.Items.Add(ListOfGender[i].ToString());
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            StudentInformationClass.SetFullName = FullName(txtLastName.Text, txtFirstName.Text, txtMiddleInitial.Text);
            StudentInformationClass.SetStudentNo = (StudentNumber(txtStudentNo.Text));
            StudentInformationClass.SetProgram = /*(string)cbPrograms.Items[0];*/  cbPrograms.Text;

            StudentInformationClass.SetGender = cbGender.Text;
            StudentInformationClass.SetContactNo = /*(int)Convert.ToInt64*/(ContactNo(txtContactNo.Text));   /////
            StudentInformationClass.SetAge = Age(txtAge.Text);
            StudentInformationClass.SetBirthday = datePickerBirthday.Value.ToString("yyyy-MM-dd");

            

            //Console.WriteLine(StudentInformationClass.SetContactNo);

            try
            {
                if (_StudentNo != 0 && _ContactNo != 0 && _Age != 0 && _Age < 150)
                {
                    if (_FullName != null)
                    {
                        if (cbGender.SelectedItem != null)
                        {
                            if (cbPrograms.SelectedItem != null)
                            {
                                frmConfirmation frm = new frmConfirmation();
                                frm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("    Program field is empty!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("    Gender field is empty!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("    Name field is empty!");
                    }
                }
                if (_ContactNo == 0)
                {
                    MessageBox.Show("Contact number is either empty or contains invalid number format!\n" + 
                                    "Valid format contains 11 digits in total: 09xxxxxxxxxx");
                }
                if (_Age >= 150) {
                    MessageBox.Show("   Unbelievable age! ");
                }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Student number is either empty or contains invalid input");
            }
            finally
            {
                _ContactNo = 0;  //if I get errors, I won't be needing to close the form agin and agin. I can just simply update the values straignt from the form.
                _Age = 0;
                _StudentNo = 0;
                _FullName = null;

                MessageBox.Show("     Finally: Execution done. ");   //CHALLENGE 13 TRY CATCH FINALLY
            }


        }
    }
}
