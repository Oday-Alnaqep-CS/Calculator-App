using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;

namespace Calculator_App
{
    public static class ClsUi
    {
      
        public static void MakeButtonRounded(Button btn, int radius = 30)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            btn.Region = new Region(path);
        }

      
        public static void SetButtonSymbols(Form frm)
        {
            
            var controls = frm.Controls;



            void SetText(string btnName, string text)
            {
                if (frm.Controls.Find(btnName, true).FirstOrDefault() is Button btn)
                    btn.Text = text;
            }

            // «·⁄„·Ì«  «·√”«”Ì…
            SetText("btn_Add", "+");
            SetText("btn_Minus", "-");
            SetText("btn_Mult", "\u00D7"); // ◊
            SetText("btn_Div", "\u00F7");  // ˜
            SetText("btn_Equal", "=");
            SetText("btn_Delete", "\u232B"); // ”Â„ «·Õ–›
            SetText("btn_ClearAll", "AC");

            // «·⁄„·Ì«  «·⁄·„Ì…
            SetText("btn_Sin", "sin");
            SetText("btn_Cos", "cos");
            SetText("btn_Tan", "tan");
            SetText("btn_SinInverse", "sin\u207B\u00B9"); // sin?π
            SetText("btn_CosInverse", "cos\u207B\u00B9"); // cos?π
            SetText("btn_TanInverse", "tan\u207B\u00B9"); // tan?π
            SetText("btn_CotInverse", "cot\u207B\u00B9"); // cot?π
            SetText("btn_SecInvers", "sec\u207B\u00B9"); // sec?π

            // «·Ã–Ê— Ê«·√””
            SetText("btn_Square", "x\u00B2");     // x≤
            SetText("btn_Cube", "x\u00B3");       // x≥
            SetText("btn_PowerN", "x\u207F");     // x?
            SetText("btn_Sqrt", "\u221Ax");       // ?x
            SetText("btn_CubeRoot", "\u221Bx");   // ?x
            SetText("btn_Floor", "\u230Ax\u230B");
            SetText("btn_Ceil", "\u2308x\u2309");
            // —„Ê“ ≈÷«›Ì…
            SetText("btn_PI", "\u03C0");          // ?
            SetText("btn_Factorial", "x!");
            SetText("btn_Log", "log");
            SetText("btn_Ln", "ln");
            SetText("btn_Abs", "abs");
            SetText("btn_Mod", "Mod");            // √Ê “— btn_Left ﬂ„« ”„Ì Â ”«»ﬁ«

            // «·√”Â„ («·√“—«— «· Ì ŸÂ—  ›ÌÂ« ⁄·«„… ø ›Ì ’Ê— ﬂ)
            SetText("btn_Up", "\u25B2");          // ?
            SetText("btn_Down", "\u25BC");        // ?
            SetText("btn_Left", "\u25C5");        // ?
            SetText("btn_Right", "\u25BA");       // ?
        }

        
        public static void ApplyFullTheme(Control parent)
        {
            Font mathFont = new Font("Segoe UI Symbol", 12, FontStyle.Bold);
            Font mainFont = new Font("Tahoma", 18, FontStyle.Bold);

            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.ForeColor = Color.White;
                    btn.Cursor = Cursors.Hand;

                
                    MakeButtonRounded(btn);

                  
                    if (btn.Name.Contains("Add") || char.IsDigit(btn.Name.Last()))
                        btn.Font = mainFont;
                    else
                        btn.Font = mathFont;
                }

                if (c.HasChildren) ApplyFullTheme(c); 
            }
        }
    }
}