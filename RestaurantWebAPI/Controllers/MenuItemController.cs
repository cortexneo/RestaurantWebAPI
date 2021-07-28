using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models;
using System;
using System.Collections.Generic;

namespace RestaurantWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MenuItemController> _logger;

        public MenuItemController(AppDbContext context, ILogger<MenuItemController> logger = null)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MenuItem>> GetAll()
        {
            return Ok(_context.Set<MenuItem>());
        }

        [HttpGet("GetMenuItemById")]
        public ActionResult<MenuItem> GetSpecificMenuItem(int id)
        {
            return Ok(_context.Set<MenuItem>().Find(id));
        }

        [HttpPost]
        public ActionResult Create(string name, double price)
        {
            try
            {
                var menuItem = new MenuItem
                {
                    Name = name,
                    Price = price,
                };

                _context.Set<MenuItem>().Add(menuItem);
                _context.SaveChanges();

                return Ok(menuItem);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        [HttpPut]
        public ActionResult Update(int id, string name, double price)
        {
            try
            {
                var itemToUpdate = _context.Set<MenuItem>().Find(id);

                if (itemToUpdate != null)
                {
                    itemToUpdate.Name = name;
                    itemToUpdate.Price = price;

                    _context.Entry<MenuItem>(itemToUpdate)
                        .CurrentValues
                        .SetValues(itemToUpdate);

                    _context.SaveChanges();
                    return Ok(itemToUpdate);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var menuItemToRemove = _context.Set<MenuItem>().Find(id);

                if (menuItemToRemove != null)
                {
                    _context.Set<MenuItem>().Remove(menuItemToRemove);
                    _context.SaveChanges();
                    return Ok();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
    }
}
