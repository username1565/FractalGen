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
		//who, which, how, [do it]
        static string[] who = {
			"рассвет", "ропот", "рот", "пенис", "суд", "день", "фрактал", "паскудник", "дебил", 
			"обоссан", "паршивец", "тварь", "там", 
			"где-то", "как же", "но, ", "почему", "это же", "дизайнер", "экстремист", "наркоман",
			"лезвие", "слегка", "порноактёр", "чебурек", "человек", "бетон", "волюнтарист", "щелчок", "рубака",
			"групповод", "медвежоночек", "шахматист", "децибел", "яблочник", "плотоход",
			"биточек", "профсоюз", "самолёт-цистерна", "гигантомания", "авиасекстант", "химизатор", "синтез", "сахарин", 
        };
        static string[] which = {
			"травленый", "ярмарочный",
			"двадцатиградусный", "горько-солёный", "садочный", "заседательский", "просветительский",
			"безмятежный", "неистовый", "паршивый", "обоссаный", "грядущий", "бредущий", "милый",
			"усталый", "нормальный", "паскудный", "растрёпанный", "библиотечный", "убийственный", "позорный",
			"безмозглый", "настроенный", "настороженный", "подлинный", "остающийся", "несмеявшийся", "отсталый", "просящий",
			"восьмилетний","неохотно","частнопрактикующий","грудастый","взлетающий",
        };
		static string[] how = {
			"на рассвете", "неистово",  "пожевывая", "устало", "подлинно", "нормально", "просветительски", "безмозгло",
			"размыто", "скомканно", "дёрганно", "головкой", "неловко", "зашкварно", "тайно", "открыто",
			"угарно", "попарно", "топорно", "чопорно", "нанотехнологично", "прилично", "лично", "отлично",
			"закрыто",  "говнисто", "с лопаты", "нормально", "вовек", "троллируя", 
			"равноправно","электротехнично","псевдоморфозно"
        };
		static string[] doit = {
			"проткнул", "причалил",
			"обоссал", "лизал", "топтал", "таскал", "желал", "бежал", "накалывал", "ловил", "гнобил", "топил",
			"душил", "ушел", "настал", "хочет", "боится", "ест", "живёт", "умрёт", "омичевал", "напугал", "клонировал",
			"разыскивал", "пришел", "обблевал", "насвистывает", "отстал", "целовал", "спит", "убит", "продернулся",
			"самоутешился","поделился","раскрасил","вмешался","надкусил",
		};

        public string Generate(int seed)
        {
            var r = new Random(seed);
            return 	(
							who[r.Next()%who.Length]	+	" "	+	which[r.Next()%which.Length]	+	" "
						+ 	how[r.Next()%how.Length]	+	" "	+	doit[r.Next()%doit.Length]
					);		//generate text string
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

            colors = new Color[2];
            colors[0] = Color.FromArgb(r.Next()|0x808080);
            colors[1] = Color.FromArgb(r.Next()&0x3f3f3f);
            Frac(g, w/2, h/2, h/5, 0, 0);

			var top_text = seed.ToString() + " fractalgen v1.2";			//text in the top
			var random_string = new TextGen().Generate(seed);				//text in the bottom
			
			//Fill the background for text in the top...
			//For font size 12 any symbol have width lesser than 10px and heigth 18px.
			g.FillRectangle(
				new SolidBrush(Color.White),		//color - white, by default
				5,									//start position for width
				5,									//start position for heigth
				(top_text.Length*10),				//10 pixels for each symbol
				18									//background height - 18 pixels.
			);

			//Fill the background for text in the bottom...
			//For font size 22 any symbol have width lesser than 19px and heigth 33px.
			g.FillRectangle(
				new SolidBrush(Color.White),	//default - white
				(w/3-w/12),						//start width
				h - 64,							//start position for heigth
				(random_string.Length*19),		//width pixels
				33								//height - 33 pixels.
			);
			
			g.DrawString(top_text, new Font(FontFamily.GenericMonospace, 12), new SolidBrush(Color.Black), 5, 5);	//top text

            g.DrawString(random_string, 
                new Font(FontFamily.GenericMonospace, 22), new SolidBrush(Color.Black), w / 3 - w / 12, h - 64);	//bottom text
			
            b.Save(Guid.NewGuid().ToString() + ".png"); //save picture as png-file
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
