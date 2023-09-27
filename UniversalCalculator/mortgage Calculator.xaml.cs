using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class mortgage_Calculator : Page
	{
		public mortgage_Calculator()
		{
			this.InitializeComponent();
		}

		private void calculateButton_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				// Parse user input from text boxes
				double principal = double.Parse(principalTextBox.Text);
				int numberOfYears = int.Parse(yearsTextBox.Text);
				int numberOfMonths = int.Parse(monthsTextBox.Text);

				double annualInterestRate;
				if (double.TryParse(annualInterestRateTextBox.Text, out annualInterestRate))
				{
					// Convert annual interest rate to decimal and calculate the monthly rate
					double monthlyInterestRate = annualInterestRate / 12 / 100; // Assuming input is in percentage form

					// Calculate the total number of months
					int totalMonths = numberOfYears * 12 + numberOfMonths;

					// Calculate the monthly payment using the provided formula
					double numerator = principal * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths));
					double denominator = Math.Pow(1 + monthlyInterestRate, totalMonths) - 1;

					if (denominator == 0)
					{
						throw new ArgumentException("The denominator cannot be zero.");
					}

					double monthlyPayment = numerator / denominator;

					// Clear any previous error messages
					errorTextBlock.Text = "";

					// Display the results in the respective text boxes
					annualInterestRateTextBox.Text = (annualInterestRate).ToString("0.00") + "%";
					monthlyInterestRateTextBox.Text = (monthlyInterestRate * 100).ToString("0.0000") + "%";
					monthlyRepaymentTextBox.Text = monthlyPayment.ToString("0.00");
				}
				else
				{
					// Handle invalid interest rate input (e.g., display an error message)
					errorTextBlock.Text = "Invalid interest rate input.";
				}
			}
			catch (FormatException)
			{
				errorTextBlock.Text = "Invalid input. Please enter valid numbers.";
			}
			catch (Exception ex)
			{
				errorTextBlock.Text = "An error occurred: " + ex.Message;
			}
		}

		private void exitButtonReturn_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(Main_Menu));
		}
	}
}