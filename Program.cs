using System;
using System.Drawing;

namespace fractalgen
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Please, wait, generating fractal...");
            new FractalGen().GenerateToFile();
            Console.WriteLine("Done!");
        }
    }

    class TextGen
    {
        static string[] up = { 
        "рассвет", "обоссали", "гнилыми", "ропот", "рот", "пенис", "лизали", "топтали", 
        "таскали", "желали", "суд", "день", "бог", "фрактал", "бежал", "накалывал",
        "ловил", "гнобили", "топили", "душили", "целовал", "просил", "дебил", "милый", 
        "паршивый", "нормально", "паскуда", "тварь", "с тобой", "ты", "здесь", "там", 
        "пришел", "ушел", "настанет", "где-то", "как же", "если бы не", "или", "хватит", 
        "обблевали", "это же", "хотим", "боимся", "едим", "живём", "умрём", 
        "дизайнер", "экстремист", "наркоман", "омичевали", "тайно", "открыто",
        "лезвиями", "напугали", "храм", "клонировали", "разыскивали", "побольше",
        "слегка" };
        static string[] down = { 
        "рассветами", "рассветов", "настойками", "настоек", "безмятежно", "безмятежный", "неистово", "неистовый", "пожевывая",
         "насвистывая", "обоссан", "обоссаный", "всевластия", "грядущий", "бредущий", 
         "усталый", "устало", "отсталый", "отстало", "подлинный", "подлинно", "растрёпанных", "растрёпанный", "остающихся", 
         "несмеявшихся", "нормально", "нормальный", "паскуда", "паскудный", "тварь", "безмозгло", "размыто", "скомканно", "дёрганно", 
         "безмозглая", "головкой", "неловко", "духовный", "церковный", "верховный", "зашкварно", 
         "угарно", "попарно", "топорно", "чопорно", "порно", "нанотехнологично", 
         "прилично", "лично", "отлично", "фанфар", "гитар", "дискотек", "библиотек", 
         "чебурек", "человек", "убит", "закрыт", "спит", "говна", "с лопаты", 
         "божий", "грешный", "убийственный", "позорный" };

        public string Generate(int seed)
        {
            var r = new Random(seed);
            return up[r.Next()%up.Length] + " " + down[r.Next()%down.Length];
        }
    }

    class FractalGen
    {
        const int cnt = 3;
        const int maxd = 10;
        float[] len = new float[cnt];
        float[] ang = new float[cnt];
        Color col;
        Color[] colors;

        public void GenerateToFile()
        {
            var r = new Random();
            int seed = r.Next();
            r = new Random(seed);

            for (int i = 0; i < cnt; i++)
            {
                len[i] = (float)r.NextDouble()/4 + 0.5f;
                ang[i] = ((float)r.NextDouble() - 0.5f) * 360;
            }

            var w = 1500;
            var h = 1500;
            var b = new Bitmap(w, h);
            var g = Graphics.FromImage(b);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, w, h);
            g.DrawString(seed.ToString() + " v1.1 fractalgen", new Font(FontFamily.GenericMonospace, 12), new SolidBrush(Color.Black), 5, 5);

            g.DrawString(new TextGen().Generate(seed), 
                new Font(FontFamily.GenericMonospace, 22), new SolidBrush(Color.Black),w / 2 - w / 12, h - 64);


            colors = new Color[2];
            colors[0] = Color.FromArgb(r.Next()|0x808080);
            colors[1] = Color.FromArgb(r.Next()&0x3f3f3f);
            Frac(g, w/2, h/2, h/5, 0, 0);


            b.Save(Guid.NewGuid().ToString() + ".png");
        }

        static float[] Rotate(float[] p, float a)
        {
            float[] n = new float[2];
            float dtr = (float)(Math.PI / 180);
            a *= dtr;
            float ca = (float) Math.Cos(a);
            float sa = (float) Math.Sin(a);
            n[0] = p[0] * ca - p[1] * sa;
            n[1] = p[1] * ca + p[0] * sa;
            return n;
        }

        static float fLerp(float a, float b, float t)
        {
            return (a * (1-t) + b * t);
        }

        static int iLerp(int a, int b, float t)
        {
            return (int)(a * (1-t) + b * t);
        }

        static Color Lerp(Color a, Color b, float t)
        {
            return Color.FromArgb(iLerp(a.A, b.A, t), iLerp(a.R, b.R, t), iLerp(a.G, b.G, t), iLerp(a.B, b.B, t));
        }

        void Frac(
            Graphics g, 
            float x, float y, float s, float a, int d)
        {
            if (d > maxd)
                return;
            float t0 = d / (float)(maxd + 1);
            int it = (int)(t0*200);

            if (d < 4) it = 180;

            for (int i = 0; i < cnt; i++)
            {
                float[] p = new float[] { s * len[i], s * len[i] };
                p = Rotate(p, ang[i] + a);
                p[0] += x;
                p[1] += y;

                col = Lerp(colors[0], colors[1], d/(float)maxd);

                g.DrawLine(new Pen(Color.FromArgb(202-it, col), 1), x, y, p[0], p[1]);
                Frac(g, p[0], p[1], s * len[i], a + ang[i], d + 1);
            }
        }
    }
}
