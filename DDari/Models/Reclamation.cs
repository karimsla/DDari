using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDari.Models
{
    public class Reclamation
    {
        
    public int id { get; set; }

      
    public string type{get;set;}
        
    public string title{get;set;}
     
    public string explication{get;set;}


        public DateTime dateTime{get;set;}
        public string treatement{get;set;}
        public bool state{get;set;}//by default false when it will be treated it will be true

     
    public Priority priority{get;set;}
 
   // public Utilisateur user{get;set;}
    }
}