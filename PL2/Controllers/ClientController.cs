using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Services.Abstract;
using System;
using System.Linq;
using PL.Models;
using System.Collections.Generic;

namespace PL.Controllers
{
    public class ClientController : Controller
    {
        private IClientServices _clientService;
        public ClientController(IClientServices clientService)
        {
            _clientService = clientService;
        }

        #region Управление компонентами
        public ActionResult EditClientList()
        {
            List<Client> clients = _clientService.Read();
            return View(clients);
        }
        public ActionResult EditClient(int clientId) =>
            View(_clientService.ReadById(clientId));
        [HttpPost]
        public ActionResult EditClient(Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _clientService.Update(client);
                    return RedirectToAction("EditClientList");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                }
            }
            return View(client);
        }
        public ActionResult CreateClient() =>
            View(new Client());

        [HttpPost]
        public ActionResult CreateClient(Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _clientService.Create(client);
                    return RedirectToAction("EditClientList");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                }
            }
            return View(client);
        }
        public ActionResult DeleteComponent(int clientId)
        {
            _clientService.Delete(new Client() { Id = clientId });
            return RedirectToAction("EditClientList");
        }

        #endregion

    }
}
