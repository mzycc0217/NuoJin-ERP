using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 public delegate string Creatcodes();
namespace XiAnOuDeERP.MethodWay
{
   

     class CreatCode
    {
        
        private static string EncodingN = "0,1,2,3,4,5,6,7,8,9";
        private static string Encodinga = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        private static string EncodingA = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";


        private static string Encodingp = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,0,1,2,3,4,5,6,7,8,9";
       public static string CreatEncond()
        {
            string[] strArray = EncodingA.Split(',');
            string[] strArray1 = EncodingN.Split(',');
            string[] strArray2 = Encodinga.Split(',');

            Random re = new Random();

            string stp = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                int st = re.Next(strArray.Length);
                stp += strArray[st];
            }
            string stp1 = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                int st = re.Next(strArray1.Length);
                stp1 += strArray1[st];
            }
            string stp2 = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                int st = re.Next(strArray2.Length);
                stp2 += strArray2[st];
            }

            return stp + stp2 + "-" + stp1;
        }


        public static string CreatEnconds()
        {
            // string[] strArray = EncodingA.Split(',');
            //string[] strArray1 = EncodingN.Split(',');
            string[] Encodingpd = Encodingp.Split(',');

            Random re = new Random();
            string stp2 = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                int st = re.Next(Encodingpd.Length);
                stp2 += Encodingpd[st];
            }

            return stp2;
        }
    }
}