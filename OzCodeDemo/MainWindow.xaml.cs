using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OzCodeDemo.DemoClasses.Customers;

namespace OzCodeDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [ImportMany(typeof(IOzCodeDemo))]
        public IEnumerable<Lazy<IOzCodeDemo, DemoName>> OzCodeDemos { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            var container = new CompositionContainer(catalog);

            //Fill the imports of this object
            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Trace.WriteLine(compositionException.Message);
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            try
            {
                var demo = (from d in OzCodeDemos
                            where button != null && d.Metadata.Name == button.Name
                            select d.Value).Single();

                demo.Start();
            }

            catch (Exception exp)
            {
                Trace.WriteLine(exp.Message);
            }
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //1. Debugger.Break() will cause the debugger to stop even if there is not breakpoint set
            //Press F10 to go to the next line
            Debugger.Break();
            
            //2. Press F10 again to to step over the function 
            F10WillStepOverThisFunction();
            
            //3. Press F11 to step into this function
            F11WillStepIntoThisFunction();

            //5. Drag the yellow arrow to the DragTheArrowHere-line
            ThisLineShouldNotBeExecuted();

            //6. Drag the yellow arrow here
            DragTheArrowHere();

            ThisLineIsNotInterestingAtAll();

            //7. Place the cursor on the RunToCursor()-line and press Ctrl+F10
            //or right-click and select "Run to Cursor"
            //or hover over the RunToCursor()-line and click on the green-arrow that appears to the left
            RunToCursor();

            //8. Press F5 to stop debugging and continue the program
            int six = 6;
            F5WillContinue();

            var insurance = new Insurance
            {
                Id = Guid.NewGuid(),
                Customer = new Customer
                {
                    Address = new Address
                    {
                        City = "Stockholm", State = "NA", Country = "Sweden", StreetAddress = "Barks väg 15",
                        ZipCode = 17073
                    }
                }
            };
            Debugger.Break();
            //10. Hover over the insurance variable and expand insurance, Customer and Address
            //11. Right-click on the insurance variable and select QuickWatch, then expand the same parts again
            //12. Right-click on the insurance variable and select Add watch. This should open the Debug->Windows->Watch 1 window.
            //13. Go to Debug->Windows->Watch 1 and search for Stockholm. Increase the search Depth if you dont get any results
            //14. Hover over the variable six and press the pin next to the value.
            //15. Hover over the variable six and click on the value. Change it to 7

            //16. Add a breakpoint on the line "sum += i" by clicking in the grey area to the left of the line.
            //17. Press F5 to continue   
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                //18. Right-click on the red break point and select Conditions
                //19. Enter i==5 as an conditional expression (that should be true)
                //20. Press F5 again. Notice the value of i
                //21. Remove the break-point by left-clicking on it
                //    or right-click and select "Delete Breakpoint"
                //21. Add a break-point on "sum +=i"-line.Right click on it and select Actions
                //22. Enter "I={i} and Sum={sum}" as message and press close
                //23. Press F5 to continue.
                //24. Look at the output in the View->Output window. Select Debug in "Show output from".
                sum += i;
            }
            //25. This exercise is now completed. Press F5 and continue with the demos of OzCode  
            Debugger.Break();
        }

        private void F5WillContinue()
        {
            int seven = 7;
            AnotherFunctionCall();
        }

        private void AnotherFunctionCall()
        {
            int eight = 8;
            //9. Go to Debug->Windows->Call Stack to display the call stack
            //Click on the different levels (AnotherFunctionCall, F5WillContinue and Imagge_MouseDown)
            //Go to Debug->Windows->Locals to display local variables. 
            //Do they change when you select a different level in the Call stack?
            //Press F5 to continue
            Debugger.Break();
        }

        private void RunToCursor()
        {
        }

        private void ThisLineIsNotInterestingAtAll()
        {
        }

        private void ThisLineShouldNotBeExecuted()
        {
            throw new Exception("This line should not be executed!");
        }

        private void DragTheArrowHere()
        {
        }

        private void F11WillStepIntoThisFunction()
        {
            //4. Press Shift+F11 to step out of this function
            int i = 0;
            i = i + 1;
            i += 1;
            i++;
            ++i;
        }

        private void F10WillStepOverThisFunction()
        {
        }
    }

    internal class Insurance
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
    }
}
