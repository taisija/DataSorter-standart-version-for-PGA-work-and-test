using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataSorter
{
    public partial class DataSorter : Form
    {
        public DataSorter()
        {
            InitializeComponent();
        }

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            DataLoader DL;
            DataWriter DW;
            ImageParameterization IP;
            int ImagesNum = (int)numericUpDownImNum.Value;
            int matricesNum = 16;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                DL = new DataLoader(folderBrowserDialog.SelectedPath, folderBrowserDialog.SelectedPath);
                DL.ReadSourceParametersForAllCombinetions(ImagesNum, 6);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    int[,] m = new int[2, matricesNum];
                    m[0, 0] = 0;
                    m[1, 0] = 1;
                    m[0, 1] = 1;
                    m[1, 1] = 1;
                    m[0, 2] = 1;
                    m[1, 2] = -1;
                    m[0, 3] = -1;
                    m[1, 3] = 1;
                    m[0, 4] = 1;
                    m[1, 4] = -4;
                    m[0, 5] = 2;
                    m[1, 5] = -3;
                    m[0, 6] = -2;
                    m[1, 6] = 4;
                    m[0, 7] = 2;
                    m[1, 7] = 5;
                    m[0, 8] = 3;
                    m[1, 8] = 4;
                    m[0, 9] = 3;
                    m[1, 9] = 5;
                    m[0, 10] = 3;
                    m[1, 10] = 7;
                    m[0, 11] = 3;
                    m[1, 11] = 11;
                    m[0, 12] = 5;
                    m[1, 12] = 13;
                    m[0, 13] = 7;
                    m[1, 13] = 13;
                    m[0, 14] = 7;
                    m[1, 14] = 17;
                    m[0, 15] = 13;
                    m[1, 15] = 17; 
                    IP = new ImageParameterization(ImagesNum, 0, 5 + 8 * matricesNum, true);
                    IP.LoadImages("");
                    IP.Calculate_mN(2);
                    IP.Calculate_mN(3);
                    IP.Calculate_mN(4);
                    IP.CalculateDivergence();
                    IP.ParamFromCOoccurenceMatrix(m, matricesNum);
                    //IP.AddResortDataAsPercentage(DL.LoadingData, DL.DataLen, DL.ObservationNum);
                    double[][] newData = new double[DL.DataLen.Length][];
                    string[][] newDataNames = new string[1][];
                    for (int i = 0; i < ImagesNum; i++)
                    {
                        for (int k = i*325; k < ((i+1)*325); k++)
                        {
                            newData[k] = new double[IP.ParamCounter + 3];
                            for (int j = 3; j < (IP.ParamCounter+3); j++)
                            {
                                newData[k][j] = IP.TestImageResults[i][j-3];
                            }
                            newData[k][0] = i;
                            newData[k][1] = k - i * 325;
                            newData[k][2] = DL.FitnessLevel[k];
                        }
                    }
                    /*for (int i = 0; i < ImagesNum; i++)
                    {
                        for (int k = i * 325; k < ((i + 1) * 325); k++)
                        {*/
                            newDataNames[0] = new string[IP.ParamCounter/* + 3*/];
                            for (int j = 0/*3*/; j < (IP.ParamCounter /*+ 3*/); j++)
                            {
                                newDataNames[0][j] = IP.TestImageResultsNames[0][j/* - 3*/];
                            }
                            /*newDataNames[k][0] = "imageNumber";
                            newDataNames[k][1] = "filterNumber";
                            newDataNames[k][2] = "fitnessLevel";*/
                       /* }
                    }*/
                        DW = new DataWriter(saveFileDialog.FileName);
                        DW.WriteData(newData, newDataNames);
                    //DW.WriteData(IP.CorreletionT(0.5), 5 + 128 + 13);
                }
            }
        }

        private void resortButton_Click(object sender, EventArgs e)
        {
            DataLoader DL;
            DataWriter DW;
            ImageParameterization IP;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                 DL = new DataLoader(folderBrowserDialog.SelectedPath, folderBrowserDialog.SelectedPath);
                 DL.ReadSourceParametersForAllCombinetions(50, 6);
                 if (saveFileDialog.ShowDialog() == DialogResult.OK)
                 {
                     DW = new DataWriter(saveFileDialog.FileName);
                     DW.WriteData(DL.LoadingData, DL.DataLen, DL.ObservationNum);
                 }
            }
        }
    }
}
