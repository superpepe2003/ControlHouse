using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlHouse.Helpers
{
    public class DropDownLists
    {
        public DropDownLists(string titulo, string accion, string controlador)
        {
            this.Titulo = titulo;
            this.Accion = accion;
            this.Controlador = controlador;
        }
        public string Titulo { get; set; }
        public string Accion { get; set; }
        public string Controlador { get; set; }        
    }

    public static class Helpers
    {
        public static MvcHtmlString DropDown(this HtmlHelper phpelper, List<DropDownLists> Lista, string titulo,int id)
        {
            string html = " <div class=\"dropdown\"><button class=\"btn btn-secondary btn-sm dropdown-toggle\" id=\"menu1\" type=\"button\" data-toggle=\"dropdown\">";
            html += titulo;
            html += "\n" + "<span class=\"caret\"></span>";
            html += "\n" + "</button>";


            string lista= "<ul class=\"dropdown-menu\">";            
            foreach(DropDownLists c in Lista)
            {
                lista += "<li><a href= \"/" + c.Controlador + "/" + c.Accion + "/" + id + "\">" + c.Titulo + "</a></li>";
            }
            lista += "</ul></div>";


            html += "\n" + lista;

            return new MvcHtmlString(html);
        }

        public static MvcHtmlString DropDown(this HtmlHelper phpelper, List<DropDownLists> Lista, string titulo, int id, string modal)
        {
            string html = " <div class=\"dropdown\"><button class=\"btn btn-secondary btn-sm dropdown-toggle\" id=\"menu1\" type=\"button\" data-toggle=\"dropdown\">";
            html += titulo;
            html += "\n" + "<span class=\"caret\"></span>";
            html += "\n" + "</button>";


            string lista = "<ul class=\"dropdown-menu\">";
            foreach (DropDownLists c in Lista)
            {
                string botonTitulo = "btn" + c.Controlador + c.Accion;
                lista += "<li><a class=\""+ botonTitulo + "\" href= \"#" + modal + "\" data-toggle= \"modal\" data-target=\"#myModal\" data-id= \"" + id + "\">" + c.Titulo + "</a></li>";
            }
            lista += "</ul></div>";


            html += "\n" + lista;

            return new MvcHtmlString(html);
        }


        public static MvcHtmlString DropDown(this HtmlHelper phpelper, List<DropDownLists> Lista, string titulo, string id, string modal)
        {
            string html = " <div class=\"dropdown\"><button class=\"btn btn-secondary btn-sm dropdown-toggle\" id=\"menu1\" type=\"button\" data-toggle=\"dropdown\">";
            html += titulo;
            html += "\n" + "<span class=\"caret\"></span>";
            html += "\n" + "</button>";


            string lista = "<ul class=\"dropdown-menu\">";
            foreach (DropDownLists c in Lista)
            {
                string botonTitulo = "btn" + c.Controlador + c.Accion;
                lista += "<li><a class=\"" + botonTitulo + "\" href= \"#" + modal + "\" data-toggle= \"modal\" data-target=\"#myModal\" data-id= \"" + id + "\">" + c.Titulo + "</a></li>";
            }
            lista += "</ul></div>";


            html += "\n" + lista;

            return new MvcHtmlString(html);
        }
    }
}