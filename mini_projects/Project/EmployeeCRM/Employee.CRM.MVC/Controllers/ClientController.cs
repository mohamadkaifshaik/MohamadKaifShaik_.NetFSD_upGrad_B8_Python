using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeCRM.Core.DTOs;
using EmployeeCRM.MVC.Services;

namespace EmployeeCRM.MVC.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientApiClient _clientApiClient;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientApiClient clientApiClient, ILogger<ClientController> logger)
        {
            _clientApiClient = clientApiClient;
            _logger = logger;
        }

        // List all clients
        public async Task<IActionResult> Index(string searchTerm = "")
        {
            try
            {
                IEnumerable<ClientDTO> clients;
                var token = HttpContext.Session.GetString("JwtToken");

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    clients = await _clientApiClient.SearchAsync(searchTerm, token);
                    ViewBag.SearchTerm = searchTerm;
                }
                else
                {
                    clients = await _clientApiClient.GetAllAsync(token);
                }

                return View(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading clients: {ex.Message}");
                TempData["Error"] = "Error loading clients";
                return View(new List<ClientDTO>());
            }
        }

        // View client details
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var client = await _clientApiClient.GetByIdAsync(id, token);

                if (client == null)
                {
                    TempData["Error"] = "Client not found";
                    return RedirectToAction("Index");
                }

                return View(client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading client details: {ex.Message}");
                TempData["Error"] = "Error loading client details";
                return RedirectToAction("Index");
            }
        }

        // Display Create Client Form
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Create()
        {
            return View(new ClientDTO());
        }

        // Handle Create Client Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Create(ClientDTO clientDTO)
        {
            if (!ModelState.IsValid)
                return View(clientDTO);

            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                await _clientApiClient.CreateAsync(clientDTO, token);

                TempData["Success"] = "Client created successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating client: {ex.Message}");
                ModelState.AddModelError("", "Error creating client: " + ex.Message);
                return View(clientDTO);
            }
        }

        // Display Edit Client Form
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                var client = await _clientApiClient.GetByIdAsync(id, token);

                if (client == null)
                {
                    TempData["Error"] = "Client not found";
                    return RedirectToAction("Index");
                }

                return View(client);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading client for edit: {ex.Message}");
                TempData["Error"] = "Error loading client";
                return RedirectToAction("Index");
            }
        }

        // Handle Edit Client Form Submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(int id, ClientDTO clientDTO)
        {
            if (!ModelState.IsValid)
                return View(clientDTO);

            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                await _clientApiClient.UpdateAsync(id, clientDTO, token);

                TempData["Success"] = "Client updated successfully";
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating client: {ex.Message}");
                ModelState.AddModelError("", "Error updating client: " + ex.Message);
                return View(clientDTO);
            }
        }

        // Delete Client
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JwtToken");
                await _clientApiClient.DeleteAsync(id, token);

                TempData["Success"] = "Client deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting client: {ex.Message}");
                TempData["Error"] = "Error deleting client: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}