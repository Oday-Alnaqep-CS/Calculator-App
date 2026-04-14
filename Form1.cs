using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;

namespace Calculator_App
{
    public partial class Calculator : Form
    {
        private double _firstNumber = 0;
        private string _expression = "";
        private double _memory = 0;

        public Calculator()
        {
            InitializeComponent();
        }

        // =====================
        // TRIG FUNCTIONS (Degrees)
        // =====================
        public double SinAngle(double angleInDegrees)
        {
            double angle = angleInDegrees * Math.PI / 180;
            return Math.Sin(angle);
        }


        public double CosAngle(double angleInDegrees)
        {
            double angle = angleInDegrees * Math.PI / 180;
            return Math.Cos(angle);
        }

        
        public double TanAngle(double angleInDegrees)
        {
            double angle = angleInDegrees * Math.PI / 180;
            return Math.Tan(angle);
        }

        
        public double CotAngle(double value)
        {
            double angle = value * Math.PI / 180;
            return 1 / Math.Tan(angle);
        }

        
        public double SecAngle(double value)
        {
            double angle = value * Math.PI / 180;
            return 1 / Math.Cos(angle);
        }

        
        public double SinInverse(double value) => Math.Asin(value) * 180 / Math.PI;
        
        
        public double CosInverse(double value) => Math.Acos(value) * 180 / Math.PI;
        
        public double TanInverse(double value) => Math.Atan(value) * 180 / Math.PI;

        
        public double CotInverse(double value)
        {
            if (value == 0) return double.NaN;
            return Math.Atan(1 / value) * 180 / Math.PI;
        }

        
        public double SecInverse(double value)
        {
            return Math.Acos(1 / value) * 180 / Math.PI;
        }


        public double AcotInverse(double x)
        {
            return Math.Atan(1 / x) * 180 / Math.PI;
        }


        public double AsecInverse(double x)
        {
            return Math.Acos(1 / x) * 180 / Math.PI;
        }

        // =====================
        // LOG FUNCTIONS
        // =====================

        public double Log10(double x) => Math.Log10(x);
        
        public double Ln(double x) => Math.Log(x);
        
        public double Log2(double x) => Math.Log(x, 2);

        // =====================
        // MATH FUNCTIONS
        // =====================
        
        public double Abs(double number) => (number < 0) ? -number : number;

        public double ModCurrent(double numberToDivisor)
        {
            if (numberToDivisor == 0)
            {
                MessageBox.Show("Cannot divide by zero!");
                return _firstNumber;
            }

            _firstNumber %= numberToDivisor;
            return _firstNumber;
        }

        public double MyCeil(double value) => Math.Ceiling(value);
        
        public double MyFloor(double value) => Math.Floor(value);

        public int GCD(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        public int LCM(int a, int b) => (a * b) / GCD(a, b);

        public double Factorial(double n)
        {
            if (n < 0 || n != (int)n) return 0;

            double r = 1;
            for (int i = 1; i <= (int)n; i++)
                r *= i;

            return r;
        }

        public double IntPart(double v) => (int)v;

        public double Power(double x, double n) => Math.Pow(x, n);
        
        public double Square(double x) => Math.Pow(x, 2);
        
        public double Cube(double x) => Math.Pow(x, 3);

        public double CubeRoot(double x)
        {
            return Math.Pow(x, 1.0 / 3.0);
        }
        
        public double Root(double x) => Math.Sqrt(x);

        public double Pi() => Math.PI;


        // =====================
        // INPUT
        // =====================

        private void btn_PI_Click(object sender, EventArgs e)
        {
            TxtDisplay.Text = Pi().ToString();
        }

        private void btn_M_Click(object sender, EventArgs e)
        {
            _memory += double.Parse(TxtDisplay.Text);
        }

        private void btnNumbers_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            TxtDisplay.Text += btn.Text;
            _expression += btn.Text;

        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string op = "";

            switch (btn.Name)
            {
                case "btn_Add": op = "+"; break;
                case "btn_Minus": op = "-"; break;
                case "btn_Mult": op = "*"; break;
                case "btn_Div": op = "/"; break;
            }

            if (!string.IsNullOrEmpty(_expression))
            {
                char last = _expression[_expression.Length - 1];

                if (last == '+' || last == '-' || last == '*' || last == '/')
                {
                    _expression = _expression.Substring(0, _expression.Length - 1);
                    TxtDisplay.Text = TxtDisplay.Text.TrimEnd(' ', '+', '-', '*', '/');
                }
            }

            TxtDisplay.Text += " " + op + " ";
            _expression += op;
        }

        private void btn_BracketOpen_Click(object sender, EventArgs e)
        {
            TxtDisplay.Text += "(";
            _expression += "(";
        }

        private void btn_BracketClose_Click(object sender, EventArgs e)
        {
            TxtDisplay.Text += ")";
            _expression += ")";
        }

        // =====================
        // TRIG BUTTON
        // =====================
        private void btn_Trig_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            string TringName = "";

            switch (btn.Name)
            {
                case "btn_Sin": TringName = "sin("; break;
                case "btn_Cos": TringName = "cos("; break;
                case "btn_Tan": TringName = "tan("; break;
                case "btn_Cot": TringName = "cot("; break;
                case "btn_Sec": TringName = "sec("; break;

                case "btn_SinInverse": TringName = "asin("; break;
                case "btn_CosInverse": TringName = "acos("; break;
                case "btn_TanInverse": TringName = "atan("; break;
                case "btn_CotInverse": TringName = "acot("; break;
                case "btn_SecInvers": TringName = "asec("; break;

                case "btn_Log": TringName = "log("; break;
                case "btn_Ln": TringName = "ln("; break;
                case "btn_Log2": TringName = "log2("; break;
            }

            TxtDisplay.Text += TringName;
            _expression += TringName;
        }

        // =====================
        // EVALUATOR (IMPORTANT)
        // =====================
        private double Evaluate(string expr)
        {
            expr = expr.Replace(" ", "");

            // 1. «»œ√ »«·œÊ«· «·⁄ﬂ”Ì… («·√”„«¡ «·√ÿÊ·)
            while (expr.Contains("asin(")) expr = ReplaceTrig(expr, "asin", SinInverse);
            while (expr.Contains("acos(")) expr = ReplaceTrig(expr, "acos", CosInverse);
            while (expr.Contains("atan(")) expr = ReplaceTrig(expr, "atan", TanInverse);
            while (expr.Contains("acot(")) expr = ReplaceTrig(expr, "acot", AcotInverse);
            while (expr.Contains("asec(")) expr = ReplaceTrig(expr, "asec", AsecInverse);

            // 2. À„ «·œÊ«· «·√”«”Ì… («·√”„«¡ «·√ﬁ’—)
            while (expr.Contains("sin(")) expr = ReplaceTrig(expr, "sin", SinAngle);
            while (expr.Contains("cos(")) expr = ReplaceTrig(expr, "cos", CosAngle);
            while (expr.Contains("tan(")) expr = ReplaceTrig(expr, "tan", TanAngle);
            while (expr.Contains("cot(")) expr = ReplaceTrig(expr, "cot", CotAngle);
            while (expr.Contains("sec(")) expr = ReplaceTrig(expr, "sec", SecAngle);

            // 3. «··Ê€«—Ì „«  («·√ÿÊ· √Ê·«)
            while (expr.Contains("log2(")) expr = ReplaceLog(expr, "log2", Log2);
            while (expr.Contains("log(")) expr = ReplaceLog(expr, "log", Log10);
            while (expr.Contains("ln(")) expr = ReplaceLog(expr, "ln", Ln);

            DataTable dt = new DataTable();
            return Convert.ToDouble(dt.Compute(expr, ""));
        }

        private string ReplaceTrig(string expr, string name, Func<double, double> func)
        {
            int start = expr.LastIndexOf(name + "(");
            int open = start + name.Length + 1;
            int close = FindClose(expr, open);

            string inside = expr.Substring(open, close - open);

            double val = Convert.ToDouble(new DataTable().Compute(inside, ""));

            double result = func(val);

            if (double.IsNaN(result) || double.IsInfinity(result))
                throw new Exception("Invalid input");

            return expr.Substring(0, start) + result + expr.Substring(close + 1);
        }

        private string ReplaceLog(string expr, string name, Func<double, double> func)
        {
            int start = expr.LastIndexOf(name + "(");
            int open = start + name.Length + 1;
            int close = FindClose(expr, open);

            string inside = expr.Substring(open, close - open);
            double val = Evaluate(inside);

            double result = func(val);

            return expr.Substring(0, start) + result + expr.Substring(close + 1);
        }

        private int FindClose(string expr, int open)
        {
            int depth = 0;

            for (int i = open; i < expr.Length; i++)
            {
                if (expr[i] == '(') depth++;
                else if (expr[i] == ')')
                {
                    depth--;

                    if (depth == 0)
                        return i;
                }
            }

            return expr.Length - 1;
        }   
        // =====================
        // EQUAL BUTTON
        // =====================
        private void btn_Equle_Click(object sender, EventArgs e)
        {
            try
            {
                int open = _expression.Count(c => c == '(');
                int close = _expression.Count(c => c == ')');

                for (int i = 0; i < open - close; i++)
                    _expression += ")";

                var result = Evaluate(_expression);

                double res = Convert.ToDouble(result);

                string cleanResult = res.ToString("0.################");

                TxtDisplay.Text = $"{_expression} = {cleanResult}";
                _expression = cleanResult;
               // _expression = result.ToString();
            }
            catch
            {
                TxtDisplay.Text = "Error";
                _expression = "";
            }
        }

        private void btn_ClearAll_Click(object sender, EventArgs e)
        {

            TxtDisplay.ForeColor = Color.Black;
            TxtDisplay.Text = "";
            _expression = "";
            _firstNumber = 0;
        }

        private void btn_Delete_Click_1(object sender, EventArgs e)
        {
            if (TxtDisplay.Text.Length > 0)
                TxtDisplay.Text = TxtDisplay.Text.Substring(0, TxtDisplay.Text.Length - 1);

            if (_expression.Length > 0)
                _expression = _expression.Substring(0, _expression.Length - 1);
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            ClsUi.SetButtonSymbols(this);
            ClsUi.ApplyFullTheme(this);

            TxtDisplay.Font = new Font("Segoe UI", 15);
            TxtDisplay.TextAlign = HorizontalAlignment.Left;

            btn_GCD.ForeColor = Color.Black;
        }

        private void btnOPerationsPow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            double CurrentVal = double.Parse(TxtDisplay.Text);
            double result = 0;

            switch (btn.Name)
            {
                case "btn_Square": result = Square(CurrentVal); break;
                case "btn_Sqrt": result = Root(CurrentVal); break;
                case "btn_Cube": result = Cube(CurrentVal); break;
                case "btn_CubeRoot": result = CubeRoot(CurrentVal); break;
                case "btn_PowerN": result = Power(CurrentVal, 2); break;

                case "btn_Log": result = Log10(CurrentVal); break;
                case "btn_Ln": result = Ln(CurrentVal); break;
                case "btn_Log2": result = Log2(CurrentVal); break;
                case "btn_PI": result = Pi(); break;
                case "btn_IntPart": result = IntPart(CurrentVal); break;
                case "btn_Mod": result = ModCurrent(CurrentVal); break;
                case "btn_Factorial": result = Factorial(CurrentVal); break;
                case "btn_Ceil": result = MyCeil(CurrentVal); break;
                case "btn_Floor": result = MyFloor(CurrentVal); break;
                case "btn_Abs": result = Abs(CurrentVal); break;
            }

            TxtDisplay.Text = result.ToString();
            _memory = result;
        }

        private void btn_Recall_Click(object sender, EventArgs e)
        {
            TxtDisplay.Text = _memory.ToString();
        }

        private void btn_Dot_Click(object sender, EventArgs e)
        {
            string lastNumber = _expression.Split('+', '-', '*', '/').Last();

            if (lastNumber.Contains("."))
                return;

            TxtDisplay.Text += ".";
            _expression += ".";
        }

        private void btn_2nd_Click(object sender, EventArgs e)
        {
            TxtDisplay.ForeColor = Color.Red;
            TxtDisplay.Text = "I'll be here Soon";
            
        }

        private void TxtDisplay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}