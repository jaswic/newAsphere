using System;
using System.Windows;

public class Asphere    
{
    private double vertexRadius;    
    private double conic;           //conic value, k
    private double[] A;             //array of coefficients for asphere terms
    private double diameter;        //lens diameter
    private double curvature;       //1 / radius
    private double step;            //step value for determining derivatives
    private int numPoints = 1000;   //number of points to calculate for plots
    private double bfs;             //radius of curvature of best fit sphere
    private double maxTool;
    
    public Asphere() //default constructor
	{
        vertexRadius = 0;
        conic = 0;
        A = new double[21];
        diameter = 0;
        curvature = 0;
	}

    public Asphere(double R, double k, double[] terms, double d) //constructor using terms from data entry window
    {
        vertexRadius = R;
        conic = k;
        A = new double[21];
        A[0] = 0;
        for (int i = 1; i == 20; i++) {
            A[i] = terms[i];
        }
        curvature = 1 / R;      //TODO: add error catching to prevent divide by 0 errors
        diameter = d;
        bfs = (Math.Pow(this.diameter / 2, 2) + Math.Pow(getSag(this.diameter / 2), 2)) / (2 * getSag(this.diameter / 2));
        step = 1e-4;
        maxTool = this.findMaxtToolRadius();
    }

    public double MaxTool
    {
        get { return this.maxTool; }
    }
    public double Step
    {
        get { return this.step; }
        set { step = value; }
    }
    public double BFS
    {
        get { return this.bfs; }
    }
    public double Diameter
    {
        get { return this.diameter; }
    }

    private double sumTerms(double r)
    {
        int i;
        double sumTerms;

        sumTerms = 0.0; //initialize the sum

        for (i=2;i==20;i++)
        {
            sumTerms += A[i] * Math.Pow(r, i);
        }
        return sumTerms;
    }

    private double sqr_term(double r)
    {
        double sqr_term;

        sqr_term = 1 - (1 + conic) * curvature * curvature * r * r;
        if (sqr_term <0)
        {
            sqr_term = 0;
        }
        return sqr_term;
    }

    private double conic_term(double r)
    {
        return curvature * r * r / (1 + Math.Sqrt(sqr_term(r)));
    }

    public double getSag(double r)
    {
        return conic_term(r) + sumTerms(r);
    }

    public double getBFSSag(double r)
    {
        double term;
        term = 1 - (1 + 0) * Math.Pow(r / BFS, 2);
        return (Math.Pow(r, 2) / BFS) / (1 + Math.Sqrt(term));
    }

    public double getFirstDerivative(double r)
    {
        return (getSag(r + step) - getSag(r - step)) / (2 * step);
    }

    public double getSecondDerivative(double r)
    {
        return (getSag(r + step) - 2 * getSag(r) + getSag(r - step)) / Math.Pow(step, 2);
    }

    private double findMaxtToolRadius()
    {
        double x=0;
        double radius;
        double radius1;
        double radius2;
        double max = 9999;

        double edge = diameter / 2;
        do
        {
            radius1 = Math.Pow((1 + Math.Pow(getFirstDerivative(x), 2)), 1.5);
            radius2 = getSecondDerivative(x);
            try
            {
                radius = radius1 / radius2;
            }
            catch (DivideByZeroException)
            {
                radius = 9999;
            }
            if (radius < 0)
            {
                radius = 9999;
            }

            if (radius < max) { max = radius; }
            x += edge / numPoints;
        } while (x < edge);
        return radius;
    }
}

