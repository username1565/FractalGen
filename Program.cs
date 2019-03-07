using System;
using System.Drawing;					//draw Image
using System.Collections.Generic;		//to using "List"
using System.IO;						//using "File" to check words.txt.

namespace fractalgen
{
	class Program
	{
		static void Main(string[] args)	//two arguments can be specified - PNG width and height
		{
			//display info with "first two arguments setting width and height for PNG"
			System.Console.WriteLine(	"\n=================================================================\n"+
										">fractalgen.exe [PNG-width number, pixels] [PNG-height number, pixels]\n");
			System.Console.WriteLine(	">fractalgen.exe 1024 768"+
										"\n=================================================================\n");
		
		/*
			// The Length property provides the number of array elements
			System.Console.WriteLine("parameter count = {0}", args.Length);	//show number of arguments
			for (int i = 0; i < args.Length; i++)	//for each argument
			{System.Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);} //just display this...
		*/		
			//by default, PNG resolution is 1920x1080 (FullHD), 	if not specified in arguments...
			int width 	= 	1920;
			int height 	= 	1080;

			if(args.Length>2){ //if more than two arguments specified...
				System.Console.WriteLine("Too many arguments specified...\nSTOP!");	//show error and stop program...
			}
			else{
				if(args.Length>=1){	//if more than 1 arguments specified this containing width
					//check is next arguments an integers?..
					int temp_integer;													//define temp integer
					bool isNumericWidth = int.TryParse(args[0], out temp_integer);		//is numeric? true, false
			
					if(isNumericWidth==true){											//if numeric
						width = int.Parse(args[0]);										//to int
					}
					else{//leave default value and show error...
						System.Console.WriteLine(	"Width argument - not a number, this was been specified incorrectly. \n"+
													"PNG width will be default: {0}",
													width
						);
					}
			
					if(args.Length==2){ //if second argument specified
						bool isNumericheight = int.TryParse(args[1], out temp_integer);	//is numeric? true, false
						if(isNumericheight==true){										//check this
							height = int.Parse(args[1]);	 							//write as integer
						}
						else{//leave default value end show error...
							System.Console.WriteLine(
														"Height argument - not a number, this was been specified incorrectly. \n"+
														"PNG height will be default: {0}",
														height
							);
						}
					}
				}
			
				//generate fractal PNG, with this width and height
				System.Console.WriteLine("Please, wait, generating fractal...");
				new FractalGen().GenerateToFile(ref width, ref height);
				System.Console.WriteLine("Done!");
			}
			Console.ReadKey();
		}
	}

	class TextGen
	{
		//who, which, how, [do it]
		public static List<string> who = new List<string>(){};						//define empty lists
		public static List<string> which = new List<string>(){};
		public static List<string> how = new List<string>(){};
		public static List<string> doit = new List<string>(){};
		
		//Defatul values
		public static string[] who_default = 	new string[] {						//who
			"хомячина", "рассвет", "ропот", "рот", "пенис", "суд", "день", "фрактал", "паскудник", "дебил", 
			"обоссан", "паршивец", "тварь", "там", 
			"где-то", "как же", "но,", "почему", "это же", "дизайнер", "экстремист", "наркоман",
			"лезвие", "слегка", "порноактёр", "чебурек", "человек", "бетон", "волюнтарист", "щелчок", "рубака",
			"групповод", "медвежоночек", "шахматист", "децибел", "яблочник", "плотоход",
			"биточек", "профсоюз", "самолёт-цистерна", "гигантоман", "авиасекстант", "химизатор", "синтез", "сахарин",
			"мудило"
		};
		public static string[] which_default =	new string[] {						//which
			"травленый", "ярмарочный",
			"двадцатиградусный", "горько-солёный", "садочный", "заседательский", "просветительский",
			"безмятежный", "неистовый", "паршивый", "обоссаный", "грядущий", "бредущий", "милый",
			"усталый", "нормальный", "паскудный", "растрёпанный", "библиотечный", "убийственный", "позорный",
			"безмозглый", "настроенный", "настороженный", "подлинный", "остающийся", "несмеявшийся", "отсталый", "просящий",
			"восьмилетний","неохотно","частнопрактикующий","грудастый","взлетающий","упёртый","всратый","задрыпанный"
		};
		public static string[] how_default =	new string[] {						//how
			"на рассвете", "неистово",  "пожевывая", "устало", "подлинно", "нормально", "просветительски", "безмозгло",
			"размыто", "скомканно", "дёрганно", "головкой", "неловко", "зашкварно", "тайно", "открыто",
			"угарно", "попарно", "топорно", "чопорно", "нанотехнологично", "прилично", "лично", "отлично",
			"закрыто",  "говнисто", "с лопаты", "нормально", "вовек", "троллируя", 
			"равноправно","электротехнично","псевдоморфозно","как ниндзя","забористо","упёрто"
		};
		public static string[] doit_default =	new string[] {						//doit
			"проткнул", "причалил",
			"обоссал", "лизал", "топтал", "таскал", "желал", "бежал", "накалывал", "ловил", "гнобил", "топил",
			"душил", "ушел", "настал", "хочет", "боится", "ест", "живёт", "умрёт", "омичевал", "напугал", "клонировал",
			"разыскивал", "пришел", "обблевал", "насвистывает", "отстал", "целовал", "спит", "убит", "продернулся",
			"самоутешился","поделился","раскрасил","вмешался","надкусил", "умотал", "ужрался","жиреет",
			"упрыгивает", "долбится", "выпендривается"
		};
		
		public static void set_default_words(){
			//Add default words
			who.InsertRange		(0, who_default);
			which.InsertRange	(0, which_default);
			how.InsertRange		(0,	how_default);
			doit.InsertRange	(0,	doit_default);			
		}
		
		public static void save_to_file(){
			List<string> verbList = new List<string>()
			{
				"who: "+String.Join("; ", who.ToArray()),
				"which: "+String.Join("; ", which.ToArray()),
				"how: "+String.Join("; ", how.ToArray()),
				"doit: "+String.Join("; ", doit.ToArray())
			};
			
			try{
				System.IO.File.WriteAllLines("words.txt", verbList);
				Console.WriteLine("words.txt successfully saved");
			}catch (Exception e){
				Console.WriteLine("Error saving words.txt: "+e.ToString());
			}			
		}

		public string Generate(int seed)
		{
			bool save = false;
			if (File.Exists("words.txt")) {												//if exists - take words from this file
				//Console.WriteLine("words.txt - The file exists.");
				string[] readText = File.ReadAllLines("words.txt");
				//Console.WriteLine("array length: "+readText.Length);
				if(readText.Length==0){
					set_default_words();
					save = true;
				}
				else{
					for(int str = 0; str<readText.Length; str++){
						//Console.WriteLine("\n\ncurrent string: "+readText[str]);
						int len = readText[str].Length;
					
						//add words from the file to empty lists
						if(readText[str].StartsWith("who: ")){
							who.InsertRange(0, readText[str].Substring(5, len-5).Split(new string[] {"; "}, StringSplitOptions.None));
						}
						else if(readText[str].StartsWith("which: ")){
							which.InsertRange(0, readText[str].Substring(7, len-7).Split(new string[] {"; "}, StringSplitOptions.None));
						}
						else if(readText[str].StartsWith("how: ")){
							how.InsertRange(0, readText[str].Substring(5, len-5).Split(new string[] {"; "}, StringSplitOptions.None));
						}
						else if(readText[str].StartsWith("doit: ")){
							doit.InsertRange(0, readText[str].Substring(6, len-6).Split(new string[] {"; "}, StringSplitOptions.None));
						}
						//set default values, if strings is invalid, and save this then
						else if(str==0){
							who.InsertRange		(0, who_default);
							save = true;
						}else if(str==1){
							which.InsertRange	(0, which_default);
							save = true;
						}else if(str==2){
							how.InsertRange		(0,	how_default);
							save = true;
						}else if(str==3){
							doit.InsertRange	(0,	doit_default);
							save = true;
						}
						else{
							Console.WriteLine("Invalid line. Rename words.txt and restart this program to see format of this file. ");
						}
					}
				}
			}else{																		//else - use default words and save this to file.
				Console.WriteLine("words.txt - file not exists. Generate this...");
				set_default_words();
				save = true;
			}
			if(save == true){save_to_file();}

			var r = new Random(seed);
			return 	(
							who[r.Next()%who.Count]	+	" "	+	which[r.Next()%which.Count]	+	" "
						+ 	how[r.Next()%how.Count]	+	" "	+	doit[r.Next()%doit.Count]
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

		public void GenerateToFile(ref int w, ref int h)//PNG width and height from main function, picture size, bytes, filesize, bitlength
		{
			System.Console.WriteLine("Generating PNG: width {0}, height {1}", w, h);

			Random rnd = new Random();
			Byte[] clr = new Byte[28];
			rnd.NextBytes(clr);
		
			var r = new Random();
			int seed = r.Next();
			r = new Random(seed);

			for (int i = 0; i < cnt; i++)
			{
				len[i] = (float)r.NextDouble()/4 + 0.5f;
				ang[i] = ((float)r.NextDouble() - 0.5f) * 360;
			}

			var b = new Bitmap(w, h);
			var g = Graphics.FromImage(b);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			var square_side = (r.Next()%64)+1;									//random side of square from 1 to 64
			for(
				int i = 0-(r.Next()%square_side)+1;	//random shifting first square
				i<=w;									//up to width
				i = i+square_side						//add square
			){
				for(
					int j=0-(r.Next()%square_side)+1;	//random shifting first square
					j<=h;								//up to height
					j = j+square_side					//add square
				){
					//filling squares for each (j column)
					g.FillRectangle(
						new SolidBrush(
							Color.FromArgb(				//random RGBA-color
								//r.Next()%255/8,		//high dynamic transparency
								32,						//static transparent value (87.5% transparency)
								//r.Next()%255,			//random 	r
								//r.Next()%255,			//ranodom 	g
								//r.Next()%255			//random 	b
															//Make colors lighter
								255-r.Next()%255/4,		//light R
								255-r.Next()%255/4,		//light G
								255-r.Next()%255/4		//light B
							)
						),
						i,							//start width
						j,							//start height
						square_side,				//fill square width	
						square_side					//fill square height
					);
				}
			}
			//transparent background filled...

			//fill fractal
			
			colors = new Color[2];
//			colors[0] = Color.FromArgb(r.Next()|0x808080);
//			colors[1] = Color.FromArgb(r.Next()&0x3f3f3f);

			//random two rgba colors - for fractal
			colors[0] = Color.FromArgb(
				//r.Next()%255,
				255-r.Next()%255/8,							//low transparency
				r.Next()%255, r.Next()%255, r.Next()%255	//R, G, B
			);	
			colors[1] = Color.FromArgb(
				//r.Next()%255,
				255-r.Next()%255/8,							//low transparency
				r.Next()%255, r.Next()%255, r.Next()%255	//rgb
			);

			//draw fractal
			Frac(g, w/2, h/2, h/5, 0, 0);
			//fractal filled.
			
			//generate text
			var top_text = seed.ToString() + " fractalgen v1.3";			//text in the top
			var random_string = new TextGen().Generate(seed);				//text in the bottom

			//fill top and bottom text...
			int text_size = 22; 											//by default fontsize for text size is 22
			
/*
			//change font size, by PNG width
			if(w>=1920){text_size = 22;}
			else if(w>=1366){text_size = 18;}
			else if(w>=800){text_size = 12;}
			else if(w>=640){text_size = 10;}
			else if(w>=300){text_size = 8;}
			else if(w>=150){text_size = 8;}
			else{text_size = 0;}
*/
/*
			//Можно обойтись одной операцией деления и одной операцией округления
			text_size = (int)Math.Floor((double)((w+h)/100.0));
			text_size = (text_size>8)	?text_size :0;
*/
			//change font size, by PNG width and height
			text_size = (w+h) >> 7; 			// divide by 128 via shifting
			if (text_size < 8)	text_size = 0;


		
			var strings1 = 1;	//calculate strings for top text			//for test, generate PNG 150x150
			var strings2 = 1;	//calculate strings for bottom text
			
			if(text_size!=0){
				using(var sf = new StringFormat()				//using alignment for the top text
				{
					Alignment = StringAlignment.Center,			//center
					LineAlignment = StringAlignment.Near,		//top alignment - in the top
				})
				{
					if((double)top_text.Length*text_size/w > 1){
						strings1 = System.Convert.ToInt32(System.Math.Ceiling((double)top_text.Length*text_size/w));
					}

					//Fill the background for text in the top...
					g.FillRectangle(
						//new SolidBrush(Color.White),			//color - white, by default
						new SolidBrush(
							Color.FromArgb(						//color
								128,							//half-transparency
								255-r.Next()%255/8,				//random colors is lighter
								255-r.Next()%255/8,
								255-r.Next()%255/8
							)
						),
						(w-top_text.Length*text_size)/2,		//start position for width
						text_size,								//start position for height
						(top_text.Length*text_size),			//width, pixels
						(text_size*2)*strings1					//background height, pixels.
					);				

					g.DrawString(											//draw top text
						top_text,											//text
						new Font(FontFamily.GenericMonospace, text_size),	//font and fontsize
						new SolidBrush(
							Color.FromArgb(									//color
								255,										//no trancparency
								(r.Next()%255*8)%255,						//random colors for text will be shadow
								(r.Next()%255*8)%255,
								(r.Next()%255*8)%255
							)
						),
						new Rectangle(0, text_size, w, h),					//area
						sf													//alignment in area
					);
				}

				using(var sf = new StringFormat()				//using alignment for bottom text
				{
					Alignment = StringAlignment.Center,			//center
					LineAlignment = StringAlignment.Far,		//top alignment - in the bottom
				}){
					if(((double)(random_string.Length*text_size)/w)>1){
						strings2 = System.Convert.ToInt32(System.Math.Ceiling((double)random_string.Length*text_size/w));
					}

					//Fill the background for text in the bottom...
					g.FillRectangle(
						//lighter color with half transparency
						new SolidBrush(Color.FromArgb(128, 255-r.Next()%255/8, 255-r.Next()%255/8, 255-r.Next()%255/8)),
						(w-random_string.Length*text_size)/2,								//start width
						h - text_size*(strings2+4),											//start position for height
						random_string.Length*text_size,										//width pixels
						(text_size*(strings2+1))*2											//height pixels.
					);

					g.DrawString(															//bottom text
						random_string, 														//text
						new Font(FontFamily.GenericMonospace, text_size),					//font and fontsize
						new SolidBrush(Color.FromArgb(255, (r.Next()%255*8)%255, (r.Next()%255*8)%255, (r.Next()%255*8)%255)), //shadow color without trancparency
						new Rectangle(0, 0, w, h-text_size*2),								//area
						sf																	//alignment in area
					);
				}
			}//else if font size = 0, don't fill top and bottom text...
			//If image size low, like 150x150, and text is multistring, background for text will be multistring too.
			//top and bottom text filled with background.

		//Emulate LSB noise on bitmap - for prevent "frequency analysis" by the colors in PNG.
			Bitmap newBitmap = new Bitmap(b, b.Width, b.Height);
			Color actualColor;
			var newA = 0;				//define new argb colors
			var newR = 0;
			var newG = 0;
			var newB = 0;
			var randvalue = 0;			//define randvalue

			for (int i = 0; i < b.Width; i++)		//for each line
			{
				for (int j = 0; j < b.Height; j++)	//and each pixel in line
				{
					//get the pixel from the b image
					randvalue = r.Next();					//generate random value
					actualColor = b.GetPixel(i, j);			//get ARGB color for this pixel
					//System.Console.WriteLine("actualColor = {0}", actualColor); //display colors for this pixel. Color in actualColor.A, actualColor.R, etc...
					
					if (
						r.Next()%2==1	//if 1 and not 0
					){
						//change 1 last bit in the byte for each color
						newA = (actualColor.A!=255) ? actualColor.A+1 : actualColor.A-1; //add one bit, if color is lesser than 255 or subtract this bit
						newR = (actualColor.R!=255) ? actualColor.R+1 : actualColor.R-1;
						newG = (actualColor.G!=255) ? actualColor.G+1 : actualColor.G-1;
						newB = (actualColor.B!=255) ? actualColor.B+1 : actualColor.B-1;
				
						newBitmap.SetPixel(i, j, Color.FromArgb(newA, newR, newG, newB));	//set pixel in bitmap
						//just display color values
						//System.Console.WriteLine("\nold actualcolor = {1},\n" + "newcolor = {0},\n" + "actualColor.B = {2},\n" + "(actualColor.B!=255) = {3}\n", Color.FromArgb(newA, newR, newG, newB),	actualColor, actualColor.B,	actualColor.B<255);

						//newBitmap.SetPixel(i, j, Color.FromArgb(0,0,0,0));	//just write black pixel
					}
					else{//if 0 and not 1
						newBitmap.SetPixel(i, j, actualColor);	//leave pixel "AS IS"
						//newBitmap.SetPixel(i, j, Color.FromArgb(255,255,255,255)); //just write white pixel						
					}
				}
			}
			//bits for colors in bitmap was been changed randomly...
			
			//b.Save(Guid.NewGuid().ToString() + ".png"); 				//save previous not modified picture, as png-file
			newBitmap.Save(Guid.NewGuid().ToString() + ".png"); 		//save modified bitmap with LSB noise, as png-file.
			//now, this avaliable for comparison
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
