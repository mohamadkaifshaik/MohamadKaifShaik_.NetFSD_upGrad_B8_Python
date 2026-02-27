let eventList = JSON.parse(localStorage.getItem("events")) || [];

const eventContainer = document.getElementById("eventRow");
const totalCount = document.getElementById("totalCount");
const upcomingCount = document.getElementById("upcomingCount");
const completedCount = document.getElementById("completedCount");

function saveToStorage() {
  localStorage.setItem("events", JSON.stringify(eventList));
}

function animateValue(element, start, end, duration) {
  let startTime = null;
  function animation(currentTime) {
    if (!startTime) startTime = currentTime;
    let progress = currentTime - startTime;
    let value = Math.min(Math.floor((progress / duration) * end), end);
    element.textContent = value;
    if (progress < duration) requestAnimationFrame(animation);
  }
  requestAnimationFrame(animation);
}

function updateCounters() {
  let total = eventList.length;
  let upcoming = 0;
  let completed = 0;

  const today = new Date().toISOString().split("T")[0];

  for (let e of eventList) {
    if (e.date >= today) upcoming++;
    else completed++;
  }

  animateValue(totalCount, 0, total, 600);
  animateValue(upcomingCount, 0, upcoming, 600);
  animateValue(completedCount, 0, completed, 600);
}

function renderEvents() {
  eventContainer.innerHTML = "";

  const today = new Date().toISOString().split("T")[0];

  for (let i = 0; i < eventList.length; i++) {
    let status = eventList[i].date >= today ? "Upcoming" : "Completed";
    let badgeClass = eventList[i].date >= today ? "bg-success" : "bg-danger";

    eventContainer.innerHTML += `
<div class="col-md-6 col-lg-4">
<div class="card dashboard-card p-3">
<h5>${eventList[i].name}</h5>
<p class="mb-1"><strong>Date:</strong> ${eventList[i].date}</p>
<p class="mb-1"><strong>Venue:</strong> ${eventList[i].venue}</p>
<span class="badge ${badgeClass}">${status}</span>
<div class="text-end mt-3">
<button class="btn btn-sm btn-danger rounded-circle d-flex align-items-center justify-content-center"
        style="width:35px;height:35px;"
        onclick="deleteEvent(${i})">
    <i class="bi bi-trash-fill"></i>
</button>
</div>
</div>
</div>`;
  }

  updateCounters();
}

function deleteEvent(index) {
  eventList.splice(index, 1);
  saveToStorage();
  renderEvents();
}

document.getElementById("eventForm").addEventListener("submit", function (e) {
  e.preventDefault();

  if (!this.checkValidity()) {
    this.classList.add("was-validated");
    return;
  }

  const name = this.querySelectorAll("input")[0].value;
  const date = this.querySelectorAll("input")[1].value;
  const venue = this.querySelectorAll("input")[2].value;
  const description = this.querySelector("textarea").value;

  eventList.push({ name, date, venue, description });

  saveToStorage();
  renderEvents();

  this.reset();
  this.classList.remove("was-validated");

  bootstrap.Modal.getInstance(document.getElementById("addEventModal")).hide();
});

renderEvents();
