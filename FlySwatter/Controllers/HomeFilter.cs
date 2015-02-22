using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlySwatter.Models;

namespace FlySwatter.Helpers
{
    public static class TicketFilters 
    {
        public static IQueryable<Ticket> filterBigguns(this IQueryable<Ticket> tickets, string owned, string assigned, string project)
        {
            if (!String.IsNullOrEmpty(owned))
            {
                tickets = tickets.Where(t => t.OwnerUser.Email == owned); 
            }

            if (!String.IsNullOrEmpty(assigned))
            {
                tickets = tickets.Where(t => t.AssignedUser.Email == assigned); 
            }

            if (!String.IsNullOrEmpty(project))
            {
                tickets = tickets.Where(t => t.Project.Name == project); 
            }
            return tickets; 
        }
        public static IQueryable<Ticket> search(this IQueryable<Ticket> tickets, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToString().Trim();
                tickets = tickets.Where(t => t.Title.Contains(searchString)
                        || t.Description.Contains(searchString) 
                        || t.OwnerUser.Email.Contains(searchString) 
                        || t.Project.Name.Contains(searchString) 
                        || t.TicketType.Name.Contains(searchString) 
                    ); 
            }
            return tickets; 
        }
        public static IQueryable<Ticket> SortColumns(this IQueryable<Ticket> tickets, string sortOrder)
        {
            switch (sortOrder)
            {
                case "title":
                    tickets = tickets.OrderBy(t => t.Title);
                    break; 
                case "title_desc":
                    tickets = tickets.OrderByDescending(t => t.Title);
                    break; 
                case "created":
                    tickets = tickets.OrderBy(t => t.Created);
                    break; 
                case "created_desc":
                    tickets = tickets.OrderByDescending(t => t.Created);
                    break; 
                case "priority":
                    tickets = tickets.OrderBy(t => t.TicketPriority.Id);
                    break; 
                case "priority_desc":
                    tickets = tickets.OrderByDescending(t => t.TicketPriority.Name);
                    break; 
                case "project":
                    tickets = tickets.OrderBy(t => t.Project.Name);
                    break; 
                case "owner":
                    tickets = tickets.OrderBy(t => t.OwnerUser.Email);
                    break; 
                case "owner_desc":
                    tickets = tickets.OrderByDescending(t => t.OwnerUser.Email);
                    break; 
                case "assigned":
                    tickets = tickets.OrderBy(t => t.AssignedUser.Email);
                    break; 
                case "assigned_desc":
                    tickets = tickets.OrderByDescending(t => t.AssignedUser.Email);
                    break; 
                default:
                    tickets = tickets.OrderBy(t => t.Title); 
                    break;
            }
            return tickets; 
        }
    }
}
