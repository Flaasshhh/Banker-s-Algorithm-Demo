using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banker_s_Algorithm
{
    public partial class Bankers : Form
    {
        public Bankers()
        {
            InitializeComponent();
        }

        private void CheckButton_Click(object sender, EventArgs e)
        {
            int numProcesses = 3;
            int numResources = 4;

            int[] totalResources = new int[numResources];
            totalResources[0] = (int)TotalA.Value;
            totalResources[1] = (int)TotalB.Value;
            totalResources[2] = (int)TotalC.Value;
            totalResources[3] = (int)TotalD.Value;

            int[] availResources = new int[numResources];
            availResources[0] = (int)AvailableA.Value;
            availResources[1] = (int)AvailableB.Value;
            availResources[2] = (int)AvailableC.Value;
            availResources[3] = (int)AvailableC.Value;

            int[,] max = new int[numProcesses, numResources];
            max[0, 0] = (int)MaximumAP1.Value;
            max[1, 0] = (int)MaximumAP2.Value;
            max[2, 0] = (int)MaximumAP3.Value;
            max[0, 1] = (int)MaximumBP1.Value;
            max[1, 1] = (int)MaximumBP2.Value;
            max[2, 1] = (int)MaximumBP3.Value;
            max[0, 2] = (int)MaximumCP1.Value;
            max[1, 2] = (int)MaximumCP2.Value;
            max[2, 2] = (int)MaximumCP3.Value;
            max[0, 3] = (int)MaximumDP1.Value;
            max[1, 3] = (int)MaximumDP2.Value;
            max[2, 3] = (int)MaximumDP3.Value;


            int[,] alloc = new int[numProcesses, numResources];
            alloc[0, 0] = (int)AllocatedAP1.Value;
            alloc[1, 0] = (int)AllocatedAP2.Value;
            alloc[2, 0] = (int)AllocatedAP3.Value;
            alloc[0, 1] = (int)AllocatedBP1.Value;
            alloc[1, 1] = (int)AllocatedBP2.Value;
            alloc[2, 1] = (int)AllocatedBP3.Value;
            alloc[0, 2] = (int)AllocatedCP1.Value;
            alloc[1, 2] = (int)AllocatedCP2.Value;
            alloc[2, 2] = (int)AllocatedCP3.Value;
            alloc[0, 3] = (int)AllocatedDP1.Value;
            alloc[1, 3] = (int)AllocatedDP2.Value;
            alloc[2, 3] = (int)AllocatedDP3.Value;

            //In Case GUI Misfunctioned
            /* Console.WriteLine("Enter the total resources of R1, R2, R3 and R4:");
            for (int i = 0; i < numResources; i++)
            {
                totalResources[i] = Convert.ToInt32(Console.ReadLine());
            } 

            Console.WriteLine("Enter the available resources left from R1, R2, R3 and R4:");
            for (int i = 0; i < numResources; i++)
            {
                availResources[i] = Convert.ToInt32(Console.ReadLine());
            } 
            for (int i = 0; i < numProcesses; i++)
            {
                Console.WriteLine("Enter the maximum need of P" + (i + 1) + " for R1, R2, R3 and R4:");
                for (int j = 0; j < numResources; j++)
                {
                    max[i, j] = Convert.ToInt32(Console.ReadLine());
                }
                Console.WriteLine("Enter the current allocation of P" + (i + 1) + " for R1, R2, R3 and R4:");
                for (int j = 0; j < numResources; j++)
                {
                    alloc[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            */

            Banker ba = new Banker(max, alloc, availResources, totalResources);

            int[] requestedResourses = new int[numResources];
            int requestingProcess; 
            
            requestingProcess = (int)ProcessRequesting.Value - 1;
            requestedResourses[0] = (int)RequestedA.Value;
            requestedResourses[1] = (int)RequestedB.Value;
            requestedResourses[2] = (int)RequestedC.Value;
            requestedResourses[3] = (int)RequestedD.Value;



            if (ba.RequestResources(requestingProcess, requestedResourses))
            {
                MessageBox.Show("System is safe");
            }
            else
            {
                MessageBox.Show("System is not safe");
            }


        }

    }
}
