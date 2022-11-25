using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt2Małek51214_algorytmy_
{
    public partial class Analizator : Form
    {
        static int KMLicznikDlaHeapsort = 0;
        int KMPróbaBadawcza = 10;
        int KMMaksymalnyRozmiarTablicy = 10;
        int KMDolnaGranicaWartości = 10;
        static int KMGórnaGranicaWartości = 100;
        int[] KMTablicaDoSortowania;
        float[] KMWynikiZPomiaru;
        float[] KMWynikiAnalityczne;
        int[] KMTablicaLicznikOperacjiDominujących;

        public Analizator()
        {
            InitializeComponent();
            KMtxtMinimalnaPróbaBadawcza.Text = KMPróbaBadawcza.ToString();
            KMtxtMaksymalnyRozmiarTablicy.Text = KMMaksymalnyRozmiarTablicy.ToString();
            KMtxtDolnaGranicaPrzedziału.Text = KMDolnaGranicaWartości.ToString();
            KMtxtGórnaGranicaPrzedziału.Text = KMGórnaGranicaWartości.ToString();
            KMcmbAlgorytmDoAnalizy.SelectedIndex = 0;
        }

        private void KMbtnAkceptuj_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(KMtxtMinimalnaPróbaBadawcza.Text, out KMPróbaBadawcza))
            {
                errorProvider1.SetError(KMtxtMinimalnaPróbaBadawcza, "W zapisie próby badawczej wystąpił niedozwolony znak.");
                return;
            }
            if (!int.TryParse(KMtxtMaksymalnyRozmiarTablicy.Text, out KMMaksymalnyRozmiarTablicy))
            {
                errorProvider1.SetError(KMtxtMaksymalnyRozmiarTablicy, "W zapisie maksymalnego rozmiaru tablicy wystąpił niedozwolony znak.");
                return;
            }
            if (!int.TryParse(KMtxtDolnaGranicaPrzedziału.Text, out KMDolnaGranicaWartości))
            {
                errorProvider1.SetError(KMtxtDolnaGranicaPrzedziału, "W zapisie maksymalnego rozmiaru tablicy wystąpił niedozwolony znak.");
                return;
            }
            if (!int.TryParse(KMtxtGórnaGranicaPrzedziału.Text, out KMGórnaGranicaWartości))
            {
                errorProvider1.SetError(KMtxtGórnaGranicaPrzedziału, "W zapisie maksymalnego rozmiaru tablicy wystąpił niedozwolony znak.");
                return;
            }
            KMbtnWizualizacjaPrzedSortowaniem.Enabled = true;
            KMTablicaDoSortowania = new int[KMMaksymalnyRozmiarTablicy];
            KMWynikiZPomiaru = new float[KMMaksymalnyRozmiarTablicy];
            KMWynikiAnalityczne = new float[KMMaksymalnyRozmiarTablicy];
            KMTablicaLicznikOperacjiDominujących = new int[KMPróbaBadawcza];
            KMbtnTabelarycznaPrezentacja.Enabled = true;
            KMbtnGraficznaPrezentacja.Enabled = true;
            KMbtnResetuj.Enabled = true;
            KMbtnWizualizacjaPoSortowaniu.Enabled = true;
            KMbtnAkceptuj.Enabled = false;
            KMtxtDolnaGranicaPrzedziału.Enabled = false;
            KMtxtGórnaGranicaPrzedziału.Enabled = false;
            KMtxtMaksymalnyRozmiarTablicy.Enabled = false;
            KMtxtMinimalnaPróbaBadawcza.Enabled = false;
            KMcmbAlgorytmDoAnalizy.Enabled = false;
            KMpbKolorLinii.Visible = false;
            KMpbKolorTła.Visible = false;
            KMpbKolorLinii.BackColor = Color.Black;
            KMpbKolorTła.BackColor = Color.DarkGray;
            KMtxtGrubośćLinii.Text = "1";
            KMchartWykresKosztuCzasowego.ChartAreas["ChartArea1"].BackColor = Color.DarkGray;
            KMchartWykresKosztuCzasowego.Legends["Legend1"].BackColor = Color.DarkGray;
            KMchartWykresKosztuCzasowego.BackColor = Color.DarkGray;
        }

        private void KMbtnTabelarycznaPrezentacja_Click(object sender, EventArgs e)
        {
            KMbtnPrzedSortowaniemSprawdzian.Enabled = true;
            KMPoSortowaniuSprawdzian.Enabled = true;
            KMdgvKsiazki.Visible = false;
            KMdgvKsiazkiPo.Visible = false;
            KMtxtGrubośćLinii.Enabled = false;
            KMpbKolorLinii.Visible = false;
            KMpbKolorTła.Visible = false;
            KMbtnWizualizacjaPrzedSortowaniem.Enabled = true;
            KMbtnKolorLinii.Enabled = false;
            KMbtnKolorTła.Enabled = false;
            KMlblGrubośćLinii.Enabled = false;
            KMcmbStylLinii.Enabled = false;
            KMlblStylLinii.Enabled = false;
            KMtbGrubośćLinii.Enabled = false;
            KMtxtDolnaGranicaPrzedziału.Enabled = false;
            KMtxtGórnaGranicaPrzedziału.Enabled = false;
            KMtxtMaksymalnyRozmiarTablicy.Enabled = false;
            KMtxtMinimalnaPróbaBadawcza.Enabled = false;
            KMcmbAlgorytmDoAnalizy.Enabled = false;
            KMbtnTabelarycznaPrezentacja.Enabled = false;
            KMbtnGraficznaPrezentacja.Enabled = true;
            KMbtnWizualizacjaPoSortowaniu.Enabled = true;
            int KMLicznikOperacjiDominujących;
            float KMSumaOperacjiDominujących, KMŚredniaOperacjiDominujących;
            Random KMRandom = new Random();
            Sortowanie KMAlgorytmySortowania = new Sortowanie();
            for (int i = 0; i < KMMaksymalnyRozmiarTablicy; i++)
            {
                for (int j = 0; j < KMPróbaBadawcza; j++)
                {
                    for (int k = 0; k < KMMaksymalnyRozmiarTablicy; k++)
                    {
                        KMTablicaDoSortowania[k] = (KMRandom.Next(KMDolnaGranicaWartości, KMGórnaGranicaWartości));
                        switch (KMcmbAlgorytmDoAnalizy.SelectedIndex)
                        {
                            case 0:
                                KMLicznikOperacjiDominujących = KMAlgorytmySortowania.KMRadixSort(ref KMTablicaDoSortowania, i);
                                KMTablicaLicznikOperacjiDominujących[j] = KMLicznikOperacjiDominujących;
                                break;
                            case 1:
                                KMLicznikOperacjiDominujących = KMAlgorytmySortowania.KMCombSort(ref KMTablicaDoSortowania, i);
                                KMTablicaLicznikOperacjiDominujących[j] = KMLicznikOperacjiDominujących;
                                break;
                            default:
                                errorProvider1.SetError(KMbtnTabelarycznaPrezentacja, "Prace nad tym algorytmem nie zostały jeszcze ukończone.");
                                return;
                        }
                    }
                    KMSumaOperacjiDominujących = 0.0F;
                    for (int l = 0; l < KMPróbaBadawcza; l++)
                    {
                        KMSumaOperacjiDominujących = KMSumaOperacjiDominujących + KMTablicaLicznikOperacjiDominujących[l];
                    }
                    KMŚredniaOperacjiDominujących = KMSumaOperacjiDominujących / KMPróbaBadawcza;
                    KMWynikiZPomiaru[i] = KMŚredniaOperacjiDominujących;
                }
                for (int k = 0; k < KMMaksymalnyRozmiarTablicy; k++)
                {
                    KMdgvAnalizatorZłożonościObliczeniowej.Rows.Add();
                    KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[0].Value = k;
                    KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[1].Value = String.Format("{0:F2}", KMWynikiZPomiaru[k]);
                    KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[2].Value = k;
                    if (i % 2 == 0)
                    {
                        KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[0].Style.BackColor = Color.LightGray;
                        KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[1].Style.BackColor = Color.LightGray;
                        KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[2].Style.BackColor = Color.LightGray;
                    }
                    else
                    {
                        KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[0].Style.BackColor = Color.White;
                        KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[1].Style.BackColor = Color.White;
                        KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[2].Style.BackColor = Color.White;
                    }
                    KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    KMdgvAnalizatorZłożonościObliczeniowej.Rows[k].Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            KMdgvAnalizatorZłożonościObliczeniowej.Visible = true;
            KMdgvPosortowanaTablica.Visible = false;
            KMchartWykresKosztuCzasowego.Visible = false;
            KMbtnWizualizacjaPoSortowaniu.Enabled = true;
            KMbtnResetuj.Enabled = true;
        }

        private void KMbtnGraficznaPrezentacja_Click(object sender, EventArgs e)
        {
            KMbtnPrzedSortowaniemSprawdzian.Enabled = true;
            KMPoSortowaniuSprawdzian.Enabled = true;
            KMdgvKsiazki.Visible = false;
            KMdgvKsiazkiPo.Visible = false;
            KMtxtGrubośćLinii.Enabled = true;
            KMLicznikDlaHeapsort = 0;
            KMpbKolorLinii.Visible = true;
            KMpbKolorTła.Visible = true;
            KMbtnWizualizacjaPrzedSortowaniem.Enabled = true;
            KMtxtDolnaGranicaPrzedziału.Enabled = false;
            KMtxtGórnaGranicaPrzedziału.Enabled = false;
            KMtxtMaksymalnyRozmiarTablicy.Enabled = false;
            KMtxtMinimalnaPróbaBadawcza.Enabled = false;
            KMcmbAlgorytmDoAnalizy.Enabled = false;
            KMdgvAnalizatorZłożonościObliczeniowej.Visible = false;
            KMdgvPosortowanaTablica.Visible = false;
            KMbtnKolorLinii.Enabled = true;
            KMbtnKolorTła.Enabled = true;
            KMlblGrubośćLinii.Enabled = true;
            KMcmbStylLinii.Enabled = true;
            KMlblStylLinii.Enabled = true;
            KMtbGrubośćLinii.Enabled = true;
            KMbtnTabelarycznaPrezentacja.Enabled = true;
            KMbtnGraficznaPrezentacja.Enabled = false;
            KMbtnWizualizacjaPoSortowaniu.Enabled = true;
            int KMLicznikOperacjiDominujących;
            float KMSumaOperacjiDominujących, KMŚredniaOperacjiDominujących;
            Random KMRandom = new Random();
            Sortowanie KMAlgorytmySortowania = new Sortowanie();
            for (int i = 0; i < KMMaksymalnyRozmiarTablicy; i++)
            {
                for (int j = 0; j < KMPróbaBadawcza; j++)
                {
                    for (int k = 0; k < KMMaksymalnyRozmiarTablicy; k++)
                    {
                        KMTablicaDoSortowania[k] = (KMRandom.Next(KMDolnaGranicaWartości, KMGórnaGranicaWartości));
                        switch (KMcmbAlgorytmDoAnalizy.SelectedIndex)
                        {
                            case 0:
                                KMLicznikOperacjiDominujących = KMAlgorytmySortowania.KMRadixSort(ref KMTablicaDoSortowania, i);
                                KMTablicaLicznikOperacjiDominujących[j] = KMLicznikOperacjiDominujących;
                                break;
                            case 1:
                                KMLicznikOperacjiDominujących = KMAlgorytmySortowania.KMCombSort(ref KMTablicaDoSortowania, i);
                                KMTablicaLicznikOperacjiDominujących[j] = KMLicznikOperacjiDominujących;
                                break;
                            default:
                                errorProvider1.SetError(KMbtnTabelarycznaPrezentacja, "Prace nad tym algorytmem nie zostały jeszcze ukończone.");
                                return;
                        }
                    }
                    KMSumaOperacjiDominujących = 0.0F;
                    for (int l = 0; l < KMPróbaBadawcza; l++)
                    {
                        KMSumaOperacjiDominujących = KMSumaOperacjiDominujących + KMTablicaLicznikOperacjiDominujących[l];
                    }
                    KMŚredniaOperacjiDominujących = KMSumaOperacjiDominujących / KMPróbaBadawcza;
                    KMWynikiZPomiaru[i] = KMŚredniaOperacjiDominujących;
                    switch (KMcmbAlgorytmDoAnalizy.SelectedIndex)
                    {
                        case 0:
                            KMWynikiAnalityczne[i] = (i * (i - 1)) / 2;
                            break;
                        case 1:
                            KMWynikiAnalityczne[i] = ((i * (i - 1)) / 2);
                            break;
                        default:
                            errorProvider1.SetError(KMbtnTabelarycznaPrezentacja, "Prace nad tym algorytmem nie zostały jeszcze ukończone.");
                            return;
                    }
                }
                KMchartWykresKosztuCzasowego.Visible = true;
                KMchartWykresKosztuCzasowego.Titles["Title1"].Text = "Algorytm " + KMcmbAlgorytmDoAnalizy.SelectedItem;
                KMchartWykresKosztuCzasowego.Legends["Legend1"].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
                int[] KMRozmiarTabeli = new int[KMMaksymalnyRozmiarTablicy];
                for (int n = 0; n < KMMaksymalnyRozmiarTablicy; n++)
                    KMRozmiarTabeli[n] = n;
                KMchartWykresKosztuCzasowego.Series[0].Name = "Koszt czasowy z pomiaru";
                KMchartWykresKosztuCzasowego.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                KMchartWykresKosztuCzasowego.Series[0].Color = Color.Black;
                KMchartWykresKosztuCzasowego.Series[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                KMchartWykresKosztuCzasowego.Series[0].BorderWidth = 1;
                KMchartWykresKosztuCzasowego.Series[0].Points.DataBindXY(KMRozmiarTabeli, KMWynikiZPomiaru);

                KMchartWykresKosztuCzasowego.Series[1].Name = "Koszt pamieciowy";
                KMchartWykresKosztuCzasowego.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                KMchartWykresKosztuCzasowego.Series[1].Color = Color.Red;
                KMchartWykresKosztuCzasowego.Series[1].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
                KMchartWykresKosztuCzasowego.Series[1].BorderWidth = 1;
                KMchartWykresKosztuCzasowego.Series[1].Points.DataBindXY(KMRozmiarTabeli, KMRozmiarTabeli);
                KMbtnGraficznaPrezentacja.Enabled = false;
            }
            KMdgvAnalizatorZłożonościObliczeniowej.Visible = false;
            KMdgvPosortowanaTablica.Visible = false;
            KMchartWykresKosztuCzasowego.Visible = true;
            KMbtnWizualizacjaPoSortowaniu.Enabled = true;
            KMbtnResetuj.Enabled = true;
        }

        private void KMbtnKolorLinii_Click(object sender, EventArgs e)
        {
            ColorDialog KMcolorPicker = new ColorDialog();
            if (KMcolorPicker.ShowDialog() == DialogResult.OK)
            {
                KMchartWykresKosztuCzasowego.Series[0].Color = KMcolorPicker.Color;
                KMpbKolorLinii.BackColor = KMcolorPicker.Color;
            }
        }

        private void KMbtnKolorTła_Click(object sender, EventArgs e)
        {
            ColorDialog KMcolorPicker = new ColorDialog();
            if (KMcolorPicker.ShowDialog() == DialogResult.OK)
            {
                KMchartWykresKosztuCzasowego.BackColor = KMcolorPicker.Color;
                KMpbKolorTła.BackColor = KMcolorPicker.Color;
                KMchartWykresKosztuCzasowego.ChartAreas["ChartArea1"].BackColor = KMcolorPicker.Color;
                KMchartWykresKosztuCzasowego.Legends["Legend1"].BackColor = KMcolorPicker.Color;
            }
        }

        private void KMcmbStylLinii_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KMcmbStylLinii.SelectedIndex == 0)
                KMchartWykresKosztuCzasowego.Series[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            else if (KMcmbStylLinii.SelectedIndex == 1)
                KMchartWykresKosztuCzasowego.Series[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            else if (KMcmbStylLinii.SelectedIndex == 2)
                KMchartWykresKosztuCzasowego.Series[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
        }

        private void KMtbGrubośćLinii_Scroll(object sender, EventArgs e)
        {
            KMchartWykresKosztuCzasowego.Series[0].BorderWidth = KMtbGrubośćLinii.Value;
            KMtxtGrubośćLinii.Text = KMtbGrubośćLinii.Value.ToString();
        }

        private void KMbtnResetuj_Click(object sender, EventArgs e)
        {
            KMbtnPrzedSortowaniemSprawdzian.Enabled = true;
            KMPoSortowaniuSprawdzian.Enabled = true;
            KMdgvKsiazki.Visible = false;
            KMdgvKsiazkiPo.Visible = false;
            KMbtnWizualizacjaPrzedSortowaniem.Enabled = false;
            KMtxtGrubośćLinii.Enabled = false;
            KMLicznikDlaHeapsort = 0;
            KMpbKolorLinii.Visible = false;
            KMpbKolorTła.Visible = false;
            KMbtnKolorLinii.Enabled = false;
            KMbtnKolorTła.Enabled = false;
            KMlblStylLinii.Enabled = false;
            KMcmbStylLinii.Enabled = false;
            KMlblGrubośćLinii.Enabled = false;
            KMtbGrubośćLinii.Enabled = false;
            KMtxtDolnaGranicaPrzedziału.Enabled = true;
            KMtxtDolnaGranicaPrzedziału.Text = "10";
            KMtxtGórnaGranicaPrzedziału.Enabled = true;
            KMtxtGórnaGranicaPrzedziału.Text = "100";
            KMtxtMaksymalnyRozmiarTablicy.Text = "10";
            KMtxtMaksymalnyRozmiarTablicy.Enabled = true;
            KMtxtMinimalnaPróbaBadawcza.Enabled = true;
            KMtxtMinimalnaPróbaBadawcza.Text = "10";
            KMcmbAlgorytmDoAnalizy.Enabled = true;
            KMbtnAkceptuj.Enabled = true;
            KMbtnGraficznaPrezentacja.Enabled = false;
            KMbtnResetuj.Enabled = false;
            KMbtnTabelarycznaPrezentacja.Enabled = false;
            KMdgvAnalizatorZłożonościObliczeniowej.Visible = false;
            KMdgvPosortowanaTablica.Visible = false;
            KMchartWykresKosztuCzasowego.Visible = false;
            KMbtnWizualizacjaPoSortowaniu.Enabled = false;
            KMdgvAnalizatorZłożonościObliczeniowej.Rows.Clear();
            KMdgvPosortowanaTablica.Rows.Clear();

        }

        private void KMbtnWizualizacjaPrzedSortowaniem_Click(object sender, EventArgs e)
        {
            KMbtnPrzedSortowaniemSprawdzian.Enabled = true;
            KMPoSortowaniuSprawdzian.Enabled = true;
            KMdgvKsiazki.Visible = false;
            KMdgvKsiazkiPo.Visible = false;
            KMtxtGrubośćLinii.Enabled = false;
            KMLicznikDlaHeapsort = 0;
            KMpbKolorLinii.Visible = false;
            KMpbKolorTła.Visible = false;
            KMbtnWizualizacjaPrzedSortowaniem.Enabled = true;
            KMtxtDolnaGranicaPrzedziału.Enabled = false;
            KMtxtGórnaGranicaPrzedziału.Enabled = false;
            KMtxtMaksymalnyRozmiarTablicy.Enabled = false;
            KMtxtMinimalnaPróbaBadawcza.Enabled = false;
            KMcmbAlgorytmDoAnalizy.Enabled = false;
            KMbtnKolorLinii.Enabled = false;
            KMbtnKolorTła.Enabled = false;
            KMlblGrubośćLinii.Enabled = false;
            KMcmbStylLinii.Enabled = false;
            KMlblStylLinii.Enabled = false;
            KMtbGrubośćLinii.Enabled = false;
            KMbtnTabelarycznaPrezentacja.Enabled = true;
            KMbtnGraficznaPrezentacja.Enabled = true;
            KMbtnWizualizacjaPoSortowaniu.Enabled = false;
            Random KMRandom = new Random();
            Sortowanie KMAlgorytmySortowania = new Sortowanie();
            for (int i = 0; i < KMMaksymalnyRozmiarTablicy; i++)
            {
                for (int j = 0; j < KMPróbaBadawcza; j++)
                {
                    for (int k = 0; k < KMMaksymalnyRozmiarTablicy; k++)
                    {
                        KMTablicaDoSortowania[k] = (KMRandom.Next(KMDolnaGranicaWartości, KMGórnaGranicaWartości));
                    }
                }
                for (int k = 0; k < KMMaksymalnyRozmiarTablicy; k++)
                {
                    KMdgvPosortowanaTablica.Rows.Add();
                    KMdgvPosortowanaTablica.Rows[k].Cells[0].Value = k;
                    KMdgvPosortowanaTablica.Rows[k].Cells[1].Value = String.Format("{0, 8:F3}", KMTablicaDoSortowania[k]);
                    if (i % 2 == 0)
                    {
                        KMdgvPosortowanaTablica.Rows[k].Cells[0].Style.BackColor = Color.LightGray;
                        KMdgvPosortowanaTablica.Rows[k].Cells[1].Style.BackColor = Color.LightGray;
                    }
                    else
                    {
                        KMdgvPosortowanaTablica.Rows[k].Cells[0].Style.BackColor = Color.White;
                        KMdgvPosortowanaTablica.Rows[k].Cells[1].Style.BackColor = Color.White;
                    }
                    KMdgvPosortowanaTablica.Rows[k].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    KMdgvPosortowanaTablica.Rows[k].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                KMdgvAnalizatorZłożonościObliczeniowej.Visible = false;
                KMdgvPosortowanaTablica.Visible = true;
                KMchartWykresKosztuCzasowego.Visible = false;
                KMbtnWizualizacjaPoSortowaniu.Enabled = true;
                KMbtnWizualizacjaPrzedSortowaniem.Enabled = false;
                KMbtnResetuj.Enabled = true;

            }


        }

        private void KMtxtGrubośćLinii_TextChanged(object sender, EventArgs e)
        {
            int KMGrubośćLinii;
            if (!int.TryParse(KMtxtGrubośćLinii.Text, out KMGrubośćLinii))
            {
                errorProvider1.SetError(KMtxtGrubośćLinii, "W zapisie grubości linii wystąpił niedozwolony znak.");
                return;
            }
            errorProvider1.Dispose();
            if (KMGrubośćLinii > 10 || KMGrubośćLinii < 1)
            {
                errorProvider1.SetError(KMtxtGrubośćLinii, "Grubość linii musi mieścić się w przedziale <1, 10>");
                return;
            }
            errorProvider1.Dispose();
            KMchartWykresKosztuCzasowego.Series[0].BorderWidth = KMGrubośćLinii;
            KMtbGrubośćLinii.Value = KMGrubośćLinii;

        }

        private void KMbtnWizualizacjaPoSortowaniu_Click(object sender, EventArgs e)
        {
            KMbtnPrzedSortowaniemSprawdzian.Enabled = true;
            KMPoSortowaniuSprawdzian.Enabled = true;
            KMdgvKsiazki.Visible = false;
            KMdgvKsiazkiPo.Visible = false;
            KMtxtGrubośćLinii.Enabled = false;
            KMLicznikDlaHeapsort = 0;
            KMpbKolorLinii.Visible = false;
            KMpbKolorTła.Visible = false;
            KMbtnWizualizacjaPrzedSortowaniem.Enabled = true;
            KMtxtDolnaGranicaPrzedziału.Enabled = false;
            KMtxtGórnaGranicaPrzedziału.Enabled = false;
            KMtxtMaksymalnyRozmiarTablicy.Enabled = false;
            KMtxtMinimalnaPróbaBadawcza.Enabled = false;
            KMcmbAlgorytmDoAnalizy.Enabled = false;
            KMbtnKolorLinii.Enabled = false;
            KMbtnKolorTła.Enabled = false;
            KMlblGrubośćLinii.Enabled = false;
            KMcmbStylLinii.Enabled = false;
            KMlblStylLinii.Enabled = false;
            KMtbGrubośćLinii.Enabled = false;
            KMbtnTabelarycznaPrezentacja.Enabled = true;
            KMbtnGraficznaPrezentacja.Enabled = true;
            KMbtnWizualizacjaPoSortowaniu.Enabled = false;
            int KMLicznikOperacjiDominujących;
            Random KMRandom = new Random();
            Sortowanie KMAlgorytmySortowania = new Sortowanie();
            for (int i = 0; i < KMMaksymalnyRozmiarTablicy; i++)
            {
                for (int j = 0; j < KMPróbaBadawcza; j++)
                {
                    for (int k = 0; k < KMMaksymalnyRozmiarTablicy; k++)
                    {
                        KMTablicaDoSortowania[k] = (KMRandom.Next(KMDolnaGranicaWartości, KMGórnaGranicaWartości));
                        switch (KMcmbAlgorytmDoAnalizy.SelectedIndex)
                        {
                            case 0:
                                KMLicznikOperacjiDominujących = KMAlgorytmySortowania.KMRadixSort(ref KMTablicaDoSortowania, i);
                                KMTablicaLicznikOperacjiDominujących[j] = KMLicznikOperacjiDominujących;
                                break;
                            case 1:
                                KMLicznikOperacjiDominujących = KMAlgorytmySortowania.KMCombSort(ref KMTablicaDoSortowania, i);
                                KMTablicaLicznikOperacjiDominujących[j] = KMLicznikDlaHeapsort;
                                break;
                            default:
                                errorProvider1.SetError(KMbtnTabelarycznaPrezentacja, "Prace nad tym algorytmem nie zostały jeszcze ukończone.");
                                return;
                        }
                    }
                }
                for (int k = 0; k < KMMaksymalnyRozmiarTablicy; k++)
                {
                    KMdgvPosortowanaTablica.Rows.Add();
                    KMdgvPosortowanaTablica.Rows[k].Cells[0].Value = k;
                    KMdgvPosortowanaTablica.Rows[k].Cells[1].Value = String.Format("{0, 8:F3}", KMTablicaDoSortowania[k]);
                    if (i % 2 == 0)
                    {
                        KMdgvPosortowanaTablica.Rows[k].Cells[0].Style.BackColor = Color.LightGray;
                        KMdgvPosortowanaTablica.Rows[k].Cells[1].Style.BackColor = Color.LightGray;
                    }
                    else
                    {
                        KMdgvPosortowanaTablica.Rows[k].Cells[0].Style.BackColor = Color.White;
                        KMdgvPosortowanaTablica.Rows[k].Cells[1].Style.BackColor = Color.White;
                    }
                    KMdgvPosortowanaTablica.Rows[k].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    KMdgvPosortowanaTablica.Rows[k].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            KMdgvAnalizatorZłożonościObliczeniowej.Visible = false;
            KMdgvPosortowanaTablica.Visible = true;
            KMchartWykresKosztuCzasowego.Visible = false;
            KMbtnWizualizacjaPrzedSortowaniem.Enabled = true;
            KMbtnResetuj.Enabled = true;

        }
        class Sortowanie
        {
            public int KMRadixSort(ref int[] T, int n)
            {
                int KMLicznikOperacjiDominujących = 0;
                n = n + 1;
                for (int KMDzielnikDoOstatniejCyfry = 1; T.Max() / KMDzielnikDoOstatniejCyfry > 0; KMDzielnikDoOstatniejCyfry *= 10)
                {
                    int[] KMTablicaTymczasowa = new int[n];
                    int i;
                    int[] KMLicznik = new int[10];
                    for (i = 0; i < 10; i++)
                    {
                        KMLicznik[i] = 0;
                    }
                    for (i = 0; i < n; i++)
                    {
                        KMLicznikOperacjiDominujących++;
                        KMLicznik[(T[i] / KMDzielnikDoOstatniejCyfry) % 10]++;
                    }
                    for (i = 1; i < 10; i++)
                    {
                        KMLicznik[i] += KMLicznik[i - 1];
                    }
                    for (i = n - 1; i >= 0; i--)
                    {
                        KMLicznikOperacjiDominujących++;
                        KMTablicaTymczasowa[KMLicznik[(T[i] / KMDzielnikDoOstatniejCyfry) % 10] - 1] = T[i];
                        KMLicznik[(T[i] / KMDzielnikDoOstatniejCyfry) % 10]--;
                    }
                    for (i = 0; i < n; i++)
                    {
                        KMLicznikOperacjiDominujących++;
                        T[i] = KMTablicaTymczasowa[i];
                    }
                }
                return KMLicznikOperacjiDominujących;
            }
            public int KMCombSort(ref int[] T, int n)
            {
                double gap = T.Length;
                bool swaps = true;
                int KMLicznikOperacjiDominujących = 0;

                while (gap > 1 || swaps)
                {

                    gap /= 1.247330950103979;

                    if (gap < 1)
                        gap = 1;

                    int i = 0;
                    swaps = false;

                    while (i + gap < T.Length)
                    {
                        int igap = i + (int)gap;

                        if (T[i] > T[igap])
                        {
                            KMLicznikOperacjiDominujących++;
                            int temp = T[i];
                            T[i] = T[igap];
                            T[igap] = temp;
                            swaps = true;

                        }

                        ++i;

                    }
                }
                return KMLicznikOperacjiDominujących;
            }
        }
        struct KMFiszkaKsiazki
        {
            public int KMSygnatura;
            public string KMTytul;
            public string KMAutor;
            public int KMRokWydania;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            KMPoSortowaniuSprawdzian.Enabled = true;
            KMbtnPrzedSortowaniemSprawdzian.Enabled = false;
            KMdgvKsiazki.Visible = true;
            KMdgvKsiazkiPo.Visible = false;
            KMFiszkaKsiazki[] KMKartotekaBiblioteczna;
            KMKartotekaBiblioteczna = new KMFiszkaKsiazki[5];
            KMKartotekaBiblioteczna[0].KMSygnatura = 1;
            KMKartotekaBiblioteczna[0].KMTytul = "Beginning. Microsoft. Visual C# 2008.";
            KMKartotekaBiblioteczna[0].KMAutor = "Karli Watson, Christian Nagel, Jacob Hammer Pedersen, Jon D.Reid";
            KMKartotekaBiblioteczna[0].KMRokWydania = 2008;

            KMKartotekaBiblioteczna[1].KMSygnatura = 2;
            KMKartotekaBiblioteczna[1].KMTytul = "Ikony. Najpiękniejsze ikony w zbiorach polskich.";
            KMKartotekaBiblioteczna[1].KMAutor = " Ewa Sypnik-Pogorzelska Barbara Dabrowska-Gorska, Magdalena Jarzynka-Jendrzejewska";
            KMKartotekaBiblioteczna[1].KMRokWydania = 2018;

            KMKartotekaBiblioteczna[2].KMSygnatura = 3;
            KMKartotekaBiblioteczna[2].KMTytul = "Masza i Niedzwiedz. Czytanka dla malucha.";
            KMKartotekaBiblioteczna[2].KMAutor = "Wolodymir Kuryliuk";
            KMKartotekaBiblioteczna[2].KMRokWydania = 2019;

            KMKartotekaBiblioteczna[3].KMSygnatura = 4;
            KMKartotekaBiblioteczna[3].KMTytul = "Ostatnie historie.";
            KMKartotekaBiblioteczna[3].KMAutor = " Olga Tokarczuk";
            KMKartotekaBiblioteczna[3].KMRokWydania = 2015;

            KMKartotekaBiblioteczna[4].KMSygnatura = 5;
            KMKartotekaBiblioteczna[4].KMTytul = "Szafa .";
            KMKartotekaBiblioteczna[4].KMAutor = "Olga Tokarczuk";
            KMKartotekaBiblioteczna[4].KMRokWydania = 2016;

            for (int kgi = 0; kgi < 5; kgi++)
            {
                KMdgvKsiazki.Rows.Add();
                KMdgvKsiazki.Rows[kgi].Cells[0].Value = KMKartotekaBiblioteczna[kgi].KMSygnatura;
                KMdgvKsiazki.Rows[kgi].Cells[1].Value = KMKartotekaBiblioteczna[kgi].KMTytul;
                KMdgvKsiazki.Rows[kgi].Cells[2].Value = KMKartotekaBiblioteczna[kgi].KMAutor;
                KMdgvKsiazki.Rows[kgi].Cells[3].Value = KMKartotekaBiblioteczna[kgi].KMRokWydania;
            }
        }

        private void KMPoSortowaniuSprawdzian_Click(object sender, EventArgs e)
        {
            KMPoSortowaniuSprawdzian.Enabled = false;
            KMbtnPrzedSortowaniemSprawdzian.Enabled = true;
            KMdgvKsiazki.Visible = false;
            KMdgvKsiazkiPo.Visible = true;
            KMFiszkaKsiazki[] KMKartotekaBiblioteczna;
            KMKartotekaBiblioteczna = new KMFiszkaKsiazki[5];
            KMKartotekaBiblioteczna[0].KMSygnatura = 1;
            KMKartotekaBiblioteczna[0].KMTytul = "Beginning. Microsoft. Visual C# 2008.";
            KMKartotekaBiblioteczna[0].KMAutor = "Karli Watson, Christian Nagel, Jacob Hammer Pedersen, Jon D.Reid";
            KMKartotekaBiblioteczna[0].KMRokWydania = 2008;

            KMKartotekaBiblioteczna[1].KMSygnatura = 2;
            KMKartotekaBiblioteczna[1].KMTytul = "Ikony. Najpiękniejsze ikony w zbiorach polskich.";
            KMKartotekaBiblioteczna[1].KMAutor = " Ewa Sypnik-Pogorzelska Barbara Dabrowska-Gorska, Magdalena Jarzynka-Jendrzejewska";
            KMKartotekaBiblioteczna[1].KMRokWydania = 2018;

            KMKartotekaBiblioteczna[2].KMSygnatura = 3;
            KMKartotekaBiblioteczna[2].KMTytul = "Masza i Niedzwiedz. Czytanka dla malucha.";
            KMKartotekaBiblioteczna[2].KMAutor = "Wolodymir Kuryliuk";
            KMKartotekaBiblioteczna[2].KMRokWydania = 2019;

            KMKartotekaBiblioteczna[3].KMSygnatura = 4;
            KMKartotekaBiblioteczna[3].KMTytul = "Ostatnie historie.";
            KMKartotekaBiblioteczna[3].KMAutor = " Olga Tokarczuk";
            KMKartotekaBiblioteczna[3].KMRokWydania = 2015;

            KMKartotekaBiblioteczna[4].KMSygnatura = 5;
            KMKartotekaBiblioteczna[4].KMTytul = "Szafa .";
            KMKartotekaBiblioteczna[4].KMAutor = "Olga Tokarczuk";
            KMKartotekaBiblioteczna[4].KMRokWydania = 2016;

            /*            ref int[] T;
                        int n;
                        int KMLicznikOperacjiDominujących = 0;
                        n = n + 1;
                        for (int KMDzielnikDoOstatniejCyfry = 1; T.Max() / KMDzielnikDoOstatniejCyfry > 0; KMDzielnikDoOstatniejCyfry *= 10)
                        {
                            int[] KMTablicaTymczasowa = new int[n];
                            int i;
                            int[] KMLicznik = new int[10];
                            for (i = 0; i < 10; i++)
                            {
                                KMLicznik[i] = 0;
                            }
                            for (i = 0; i < n; i++)
                            {
                                KMLicznikOperacjiDominujących++;
                                KMLicznik[(T[i] / KMDzielnikDoOstatniejCyfry) % 10]++;
                            }
                            for (i = 1; i < 10; i++)
                            {
                                KMLicznik[i] += KMLicznik[i - 1];
                            }
                            for (i = n - 1; i >= 0; i--)
                            {
                                KMLicznikOperacjiDominujących++;
                                KMTablicaTymczasowa[KMLicznik[(T[i] / KMDzielnikDoOstatniejCyfry) % 10] - 1] = T[i];
                                KMLicznik[(T[i] / KMDzielnikDoOstatniejCyfry) % 10]--;
                            }
                            for (i = 0; i < n; i++)
                            {
                                KMLicznikOperacjiDominujących++;
                                T[i] = KMTablicaTymczasowa[i];
                            }
                        }
                        return KMLicznikOperacjiDominujących;
            */
            for (int kgi = 0; kgi < 5; kgi++)
            {
                KMdgvKsiazkiPo.Rows.Add();
                KMdgvKsiazkiPo.Rows[kgi].Cells[0].Value = KMKartotekaBiblioteczna[kgi].KMSygnatura;
                KMdgvKsiazkiPo.Rows[kgi].Cells[1].Value = KMKartotekaBiblioteczna[kgi].KMTytul;
                KMdgvKsiazkiPo.Rows[kgi].Cells[2].Value = KMKartotekaBiblioteczna[kgi].KMAutor;
                KMdgvKsiazkiPo.Rows[kgi].Cells[3].Value = KMKartotekaBiblioteczna[kgi].KMRokWydania;
            }
        }
    }
}
