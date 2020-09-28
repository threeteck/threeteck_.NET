open System.Drawing
open System.Windows.Forms
open System

type VizualizeForm () =
    inherit Form ()
    override this.OnLoad (e : EventArgs) =
        this.DoubleBuffered <- true

let MapToRange inMin inMax outMin outMax x =
    (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin

let form = new VizualizeForm(MaximizeBox = true, Text = "Mandelbrot Set Visualization", Width = 480, Height = 360)
let mutable size = (double)1.0
let maxIterations sz =
    (int)(100.0 / (sqrt sz))

let rec mandelbrotFunction iterations a b ca cb maxIter =
                if iterations < maxIter && a*a + b*b < ((double)4.0) then
                    let asquare = a*a - b*b
                    let bsquare = ((double)2.0)*a*b
                    mandelbrotFunction (iterations+1) (asquare + ca) (bsquare + cb) ca cb maxIter
                else
                    iterations
                   
let mutable startx = -((double)0.73)
let mutable starty = -((double)0.18)
let vizualizeSet (e : PaintEventArgs) =
    let g = e.Graphics
    g.Clear(Color.Black)
    let currentMaxIterations = maxIterations size
    for x in 0..form.Width do
        for y in 1..form.Height do
            let a = startx + MapToRange ((double)0.0) ((double)form.Width) -size size ((double)x)
            let b = starty + MapToRange ((double)0.0) ((double)form.Height) -size size ((double)y)
            let iterations = mandelbrotFunction 0 a b a b currentMaxIterations
            let brightness =
                match iterations with
                | _ when iterations = currentMaxIterations -> 0
                | _ -> (int)((double)(iterations) |> MapToRange 0.0 ((double)currentMaxIterations) 0.0 255.0)
            g.FillRectangle(new SolidBrush(Color.FromArgb((brightness + 10)%255, (brightness * 2)%255, (brightness + 40)%255)), x, y, 1, 1)
            
            

form.Paint.Add vizualizeSet

async { 
while true do
  do! Async.Sleep(1)
  form.Invalidate()
  size <- (size * 0.9)
  printf "%d " (maxIterations size)
} |> Async.StartImmediate

Application.Run form
