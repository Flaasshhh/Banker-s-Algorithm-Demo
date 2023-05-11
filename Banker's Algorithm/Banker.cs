using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker_s_Algorithm
{
    internal class Banker
    {

        private int[,] max;     // Maximum need matrix
        private int[,] alloc;   // Current allocation matrix
        private int[,] need;    // Need matrix (max - alloc)
        private int[] avail;    // Available resources vector
        private int[] total;
        private int numProcesses;   // Number of processes
        private int numResources;   // Number of resources

        public Banker(int[,] max, int[,] alloc, int[] avail, int[] total)
        {
            this.max = max;
            this.alloc = alloc;
            this.avail = avail;
            this.total = total;

            numProcesses = max.GetLength(0);
            numResources = max.GetLength(1);
        
            need = new int[numProcesses, numResources];
            CalculateNeed();
        }

        public bool RequestResources(int processId, int[] request)
        {
            // Check if the request is valid (request[i] <= need[processId,i])
            for (int i = 0; i < numResources; i++)
            {
                if (request[i] > need[processId, i])
                {
                    return false; // Invalid request: process is asking for more than it needs
                }
                if (request[i] > avail[i])
                {
                    return false; // Invalid request: not enough resources available
                }
            }

            // Try to allocate the requested resources and check for deadlock
            int[,] newAllocation = new int[numProcesses, numResources];
            int[,] newNeed = new int[numProcesses, numResources];
            int[] newAvail = new int[numResources];
            bool[] canFinish = new bool[numProcesses];
            for (int i = 0; i < numProcesses; i++)
            {
                for (int j = 0; j < numResources; j++)
                {
                    newAllocation[i, j] = alloc[i, j];
                    newNeed[i, j] = need[i, j];
                }
            }
            for (int i = 0; i < numResources; i++)
            {
                newAllocation[processId, i] += request[i];
                newNeed[processId, i] -= request[i];
                newAvail[i] = avail[i] - request[i];
            }

            // Check for deadlock
            int count = 0;
            bool[] finish = new bool[numProcesses];
            for (int i = 0; i < numProcesses; i++)
            {
                finish[i] = false;
            }
            while (count < numProcesses)
            {
                bool found = false;
                for (int i = 0; i < numProcesses; i++)
                {
                    if (!finish[i])
                    {
                        int j;
                        for (j = 0; j < numResources; j++)
                        {
                            if (newNeed[i, j] > newAvail[j])
                            {
                                break;
                            }
                        }
                        if (j == numResources)
                        {
                            for (int k = 0; k < numResources; k++)
                            {
                                newAvail[k] += newAllocation[i, k];
                            }
                            canFinish[i] = true;
                            finish[i] = true;
                            found = true;
                            count++;
                        }
                    }
                }
                if (!found)
                {
                    break; // Deadlock detected
                }
            }
            if (count < numProcesses)
            {
                // Deadlock detected: roll back the allocation
                for (int i = 0; i < numResources; i++)
                {
                    newAllocation[processId, i] -= request[i];
                    newNeed[processId, i] += request[i];
                }
                return false;
            }
            else
            {
                // No deadlock: update the matrices and return true
                alloc = newAllocation;
                need = newNeed;
                avail = newAvail;
                return true;
            }
        }



        private void CalculateNeed()
        {
            for (int i = 0; i < numProcesses; i++)
            {
                for (int j = 0; j < numResources; j++)
                {
                    need[i, j] = max[i, j] - alloc[i, j];
                }
            }
        }

        public bool IsSafe()
        {
            bool[] finish = new bool[numProcesses];
            int[] work = new int[numResources];

            Array.Copy(avail, work, numResources);

            while (true)
            {
                bool foundProcess = false;
                for (int i = 0; i < numProcesses; i++)
                {
                    if (!finish[i] && CheckNeed(i, work))
                    {
                        foundProcess = true;
                        finish[i] = true;
                        for (int j = 0; j < numResources; j++)
                        {
                            work[j] += alloc[i, j];
                        }
                    }
                }
                if (!foundProcess)
                {
                    break;
                }
            }

            foreach (bool f in finish)
            {
                if (!f)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckNeed(int process, int[] work)
        {
            for (int i = 0; i < numResources; i++)
            {
                if (need[process, i] > work[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
