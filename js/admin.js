// start in slider
let btnCloseOpner = document.querySelector("#btn");
let sideBar = document.querySelector("aside");
let overlaySide = document.querySelector(".overlaySide");

btnCloseOpner.addEventListener("click", () => {
    sideBar.classList.toggle("openCloseAnimation");
});

overlaySide.addEventListener("click", () => {
    sideBar.classList.toggle("openCloseAnimation");
    btnCloseOpen.classList.remove("on");
    overlaySide.classList.add("hideSideOverlay");
});

let btnCloseOpen = document.getElementById("btn");
btnCloseOpen.addEventListener("click", function () {
    if (this.className == "on") {
        this.classList.remove("on");
        overlaySide.classList.add("hideSideOverlay");
    } else {
        overlaySide.classList.remove("hideSideOverlay");
        this.classList.add("on");
    }
});

//lazyload
document.addEventListener('DOMContentLoaded', function () {
  const lazyImages = document.querySelectorAll('.lazy-load');

  const options = {
    root: null, // Use the viewport as the root
    rootMargin: '0px',
    threshold: 0.1 // 10% of the image must be visible
  };

  const observer = new IntersectionObserver(function (entries, observer) {
    entries.forEach(function (entry) {
      if (entry.isIntersecting) {
        const image = entry.target;
        const src = image.getAttribute('data-src');

        image.onload = function () {
          image.classList.add('loaded');
          observer.unobserve(image);
        };

        image.src = src;
      }
    });
  }, options);

  lazyImages.forEach(function (image) {
    observer.observe(image);
  });
});

// startin remove row from table slider
const deleteButtons = document.querySelectorAll(".deleteBtn");

deleteButtons.forEach((button) => {
    button.addEventListener("click", (event) => {
        const row = event.target.closest("tr");
        row.classList.add("fade-out");
        setTimeout(() => {
            row.remove();
        }, 500);
    });
});

// Obtener todas las opciones principales con desplegables
const opcionesCon = document.querySelectorAll(".optionSide");

// Agregar evento de clic a cada opción principal
opcionesCon.forEach(function (opcion) {
    opcion.addEventListener("click", function () {
        // Obtener el desplegable asociado a la opción
        const desplegable = opcion.querySelector(".desplegable");
        // Alternar la clase "open" para mostrar u ocultar el desplegable con transición suave
        desplegable.classList.toggle("open");
    });
});

//

const productFilterInput = document.querySelectorAll(".select");

productFilterInput.forEach((select) => {
    select.addEventListener(["input"], () => {
        const filterValue = select.value.trim().toLocaleLowerCase();

        // العثور على العنصر الأب الفوري لـ select
        const itemParent = select.parentElement.parentElement;

        let categoriesFilterItems = itemParent.querySelectorAll(
            ".categoriesFilter a"
        );
        let listSelect = itemParent.querySelector(".listSelect");

        categoriesFilterItems.forEach((item) => {
            const itemName = item.innerText.trim().toLocaleLowerCase();

            if (filterValue.length > 0) {
                listSelect.setAttribute("checked", "");
            } else {
                listSelect.removeAttribute("checked");
            }

            // العثور على العنصر الأب الفوري لـ item
            const itemParent = item.parentElement;

            if (itemName.includes(filterValue)) {
                itemParent.style.display = "block"; // عرض العنصر المطابق
            } else {
                itemParent.style.display = "none"; // إخفاء العنصر غير المطابق
            }

            item.addEventListener("click", () => {
                select.value = item.textContent;
            });
        });
    });
});


const modal = document.querySelector(".modal");
const overlay = document.querySelector(".overlay");
const btnCloseModal = document.querySelector(".close-modal");
const btnOpenModal = document.querySelector(".show-modal");

function openModal() {
  overlay.style.display = "block";

  modal.classList.remove("hiddenModal");
  overlay.classList.remove("hiddenOverlay");
}

function closeModal() {
  modal.classList.add("hiddenModal");
  overlay.classList.add("hiddenOverlay");
  setTimeout(() => {
    overlay.style.display = "none";
  }, 400);
}

// Clicking the 'Show modal' to open the modal
btnOpenModal.addEventListener("click", openModal);
// Clicking the 'X' button to close the modal
btnCloseModal.addEventListener("click", closeModal);
// Clicking the overlay to close the modal as well
overlay.addEventListener("click", closeModal);

// Pressing 'Esc' key to close the modal
document.addEventListener("keydown", function (e) {
  console.log(e.key);

  if (e.key === "Escape" && !modal.classList.contains("hidden")) {
    closeModal();
  }
});

function hideAlertDiv(id) {
    document.getElementById(id).classList.remove('hidden');
    setTimeout(function () {
        document.getElementById(id).classList.add('hidden');
    }, 3500)
}