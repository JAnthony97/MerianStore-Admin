
const parentTagSection_1 = document.querySelector('.section_1')
const canvas = document.getElementById('logoCanvas');
const ctx = canvas.getContext('2d');
const deviceHeight = window.innerHeight

/*Seccion 2, otro canvas aparte */
const parent_section_2_canvas = document.querySelector('.section_2');

let executeTextSection_2 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_2_canvas.getBoundingClientRect().top + parent_section_2_canvas.offsetHeight / 2;
    const bothCircles = this.document.querySelectorAll('.section_2 > div')
    const linesAnimate = this.document.querySelectorAll('.section_2 hr')
    const allImageAnimate = this.document.querySelectorAll('.section_2 > img')
    const allImageAnimateFlower = this.document.querySelectorAll('.section_2 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_2) {
        allImageAnimate[0].classList.add('section_2_text')
        bothCircles[0].classList.add('section_2-outer_circle')
        bothCircles[1].classList.add('section_2-inner_circle')
        allImageAnimate[1].classList.add('img_bg_stain_1')
        allImageAnimate[2].classList.add('img_bg_stain_2')
        linesAnimate[0].classList.add('lines_top_1');
        linesAnimate[1].classList.add('lines_top_2');
        linesAnimate[2].classList.add('lines_top_3');
        linesAnimate[3].classList.add('lines_top_4');
        allImageAnimateFlower[0].style.opacity = '1';
        allImageAnimateFlower[1].style.opacity = '1';
        allImageAnimateFlower[2].style.opacity = '1';
        allImageAnimateFlower[3].style.opacity = '1';
        parent_section_2_canvas.classList.add('section_resize')
        executeTextSection_2 = true
    }
});


/* Tercera Seccion del Canvas */
const parent_section_3_canvas = document.querySelector('.section_3');


const initialImageSize = 0;

let executeTextSection_3 = false

window.addEventListener("scroll", function () {
    const box2Top = parent_section_3_canvas.getBoundingClientRect().top + parent_section_3_canvas.offsetHeight / 2;
    const squaresImage = this.document.querySelectorAll('.section_3 > div')
    const imageAnimate = this.document.querySelectorAll('.section_3 > img')
    const imageAnimateFlower = this.document.querySelectorAll('.section_3 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_3) {
        squaresImage[0].classList.add('first_square')
        squaresImage[1].classList.add('second_square')
        imageAnimate[0].classList.add('img_wedding')
        imageAnimate[1].classList.add('section_3--recent_wedding')
        imageAnimate[2].classList.add('img_bg_stain_1')
        imageAnimate[3].classList.add('img_bg_stain_2')
        imageAnimateFlower[0].style.opacity = '1';
        imageAnimateFlower[1].style.opacity = '1';
        imageAnimateFlower[2].style.opacity = '1';
        imageAnimateFlower[3].style.opacity = '1';
        executeTextSection_3 = true
    }
});
// Seccion 4 
const parent_section_4_canvas = document.querySelector('.section_4');




let executeTextSection_4 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_4_canvas.getBoundingClientRect().top + parent_section_4_canvas.offsetHeight / 2;
    const textAnimate = document.querySelector('.section_4 > p')
    const linesAnimate = this.document.querySelectorAll('.section_4 hr')
    const bothCircles = this.document.querySelectorAll('.section_4 > span')
    const animateImages = this.document.querySelectorAll('.section_4 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_4 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_4) {
        bothCircles[0].classList.add('section_4-outer_circle')
        bothCircles[1].classList.add('section_4-inner_circle')
        textAnimate.classList.add('phrase_bible')
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        linesAnimate[0].classList.add('lines_top_1');
        linesAnimate[1].classList.add('lines_top_2');
        linesAnimate[2].classList.add('lines_top_3');
        linesAnimate[3].classList.add('lines_top_4');
        animateImagesFlower[0].style.opacity = '1';
        animateImagesFlower[1].style.opacity = '1';
        animateImagesFlower[2].style.opacity = '1';
        animateImagesFlower[3].style.opacity = '1';
        parent_section_4_canvas.classList.add('section_resize')
        executeTextSection_4 = true
    }
});

//Seccion 5
const parent_section_5_canvas = document.querySelector('.section_5');
const canvas_section_5 = document.getElementById('section_canvas_5');
const section_5_ctx = canvas_section_5.getContext('2d');


const get_width_section_5 = parent_section_5_canvas.offsetWidth;
const get_height_section_5 = parent_section_5_canvas.offsetHeight;

canvas_section_5.width = get_width_section_5 > 420 ? 420 : get_width_section_5;
canvas_section_5.height = deviceHeight

let executeTextSection_5 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_5_canvas.getBoundingClientRect().top + parent_section_5_canvas.offsetHeight / 2;
    const canvasVisibility = this.document.querySelector('.section_5 > canvas')
    const sectionVisibility = this.document.querySelector('.section_5 > section')
    const animateImages = this.document.querySelectorAll('.section_5 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_5 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_5) {
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        sectionVisibility.classList.add('scale_section_5')
        canvasVisibility.classList.add('canvas_section_3')
        animateImagesFlower[0].style.opacity = '1';
        animateImagesFlower[1].style.opacity = '1';
        parent_section_5_canvas.classList.add('section_resize')
        executeTextSection_5 = true
    }
});

// Seccion 6
const parent_section_6_canvas = document.querySelector('.section_6');

let executeTextSection_6 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_6_canvas.getBoundingClientRect().top + parent_section_6_canvas.offsetHeight / 2;
    const imageAnimate = this.document.querySelectorAll('.section_6 > img')
    const linesAnimate = this.document.querySelectorAll('.section_6 hr')
    const imageAnimateFlower = this.document.querySelectorAll('.section_6 > aside')
    const imageInvitationAnimate = this.document.querySelector('.section_6 > section > img')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_6) {
        imageAnimate[0].classList.add('img_bg_stain_1')
        imageAnimate[1].classList.add('img_bg_stain_2')
        imageInvitationAnimate.classList.add('scale_section_6_img')
        imageAnimateFlower[0].style.opacity = '1';
        imageAnimateFlower[1].style.opacity = '1';
        linesAnimate[0].classList.add('lines_top_1');
        linesAnimate[1].classList.add('lines_top_2');
        linesAnimate[2].classList.add('lines_top_3');
        linesAnimate[3].classList.add('lines_top_4');
        parent_section_6_canvas.classList.add('section_resize')
        executeTextSection_6 = true
    }
});
// Seccion 7
const parent_section_7_canvas = document.querySelector('.section_7');


let executeTextSection_7 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_7_canvas.getBoundingClientRect().top + parent_section_7_canvas.offsetHeight / 2;
    const animateSection = this.document.querySelector('.section_7 > section')
    const animateCanvas = this.document.querySelector('.section_7 > canvas')
    const animateImages = this.document.querySelectorAll('.section_7 > img')
    const getBtnCheck = this.document.querySelector('.section_7 > button')
    const animateImagesFlower = this.document.querySelectorAll('.section_7 > aside')
    const imageInvitation = this.document.querySelector('.section_7--container_info > img')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_7) {
        animateSection.classList.add('scale_section_5')
        animateCanvas.classList.add('canvas_section_7')
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        getBtnCheck.classList.add('section_7--button-check')
        imageInvitation.classList.add('scale_section_6_img')
        animateImagesFlower[0].style.opacity = '1';
        animateImagesFlower[1].style.opacity = '1';
        parent_section_7_canvas.classList.add('section_resize')
        executeTextSection_7 = true
    }
});

//Seccion 8
const parent_section_8_canvas = document.querySelector('.section_8');


let executeTextSection_8 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_8_canvas.getBoundingClientRect().top + parent_section_8_canvas.offsetHeight / 2;
    const animateSection = this.document.querySelector('.section_8 > section')
    const animateImages = this.document.querySelectorAll('.section_8 > img')
    const linesAnimate = this.document.querySelectorAll('.section_8 hr')
    const animateImagesFlower = this.document.querySelectorAll('.section_8 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0) {
        animateSection.classList.add('scale_section_8')
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        linesAnimate[0].classList.add('lines_top_1');
        linesAnimate[1].classList.add('lines_top_2');
        linesAnimate[2].classList.add('lines_top_3');
        linesAnimate[3].classList.add('lines_top_4');
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        parent_section_8_canvas.classList.add('section_resize')
        executeTextSection_8 = true
    }
});
//Seccion 9
const parent_section_9_canvas = document.querySelector('.section_9');

let executeTextSection_9 = false
const btnChecks = document.querySelectorAll('.section_9 > main > button')
btnChecks[0].addEventListener('click', () => {
    window.open('https://wa.link/c8ogij', '_blank')
})
btnChecks[1].addEventListener('click', () => {
    window.open('https://wa.link/c8ogij', '_blank')
})
window.addEventListener("scroll", function () {
    const box2Top = parent_section_9_canvas.getBoundingClientRect().top + parent_section_9_canvas.offsetHeight / 2;
    const animateSection = this.document.querySelector('.section_9 > section')
    const animateImages = this.document.querySelectorAll('.section_9 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_9 > aside')
    const centerImage = this.document.querySelector('.section_9--description > img')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_9) {
        animateSection.classList.add('animate_section_9')
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        btnChecks[0].classList.add('section_9--btn_check')
        btnChecks[1].classList.add('section_9--btn_check')
        centerImage.classList.add('scale_section_6_img')
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        parent_section_9_canvas.classList.add('section_resize')
        executeTextSection_9 = true
    }
});
//Seccion 10
const parent_section_10_canvas = document.querySelector('.section_10');

let executeTextSection_10 = false

window.addEventListener("scroll", function () {
    const box2Top = parent_section_10_canvas.getBoundingClientRect().top + parent_section_10_canvas.offsetHeight / 2;
    const animateImages = this.document.querySelectorAll('.section_10 > img')
    const linesAnimate = this.document.querySelectorAll('.section_10 hr')
    const bothCircles = this.document.querySelectorAll('.section_10 > div')
    const textAnimate = this.document.querySelector('.section_10 > main')
    const animateImagesFlower = this.document.querySelectorAll('.section_10 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_10) {
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        bothCircles[0].classList.add('section_10-outer_circle')
        bothCircles[1].classList.add('section_10-inner_circle')
        textAnimate.classList.add('phrase_section_10')
        btnAction.classList.add('phrase_section_10--btn_check')
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        linesAnimate[0].classList.add('lines_top_1');
        linesAnimate[1].classList.add('lines_top_2');
        linesAnimate[2].classList.add('lines_top_3');
        linesAnimate[3].classList.add('lines_top_4');
        parent_section_10_canvas.classList.add('section_resize')
        executeTextSection_10 = true
    }
});
//Seccion 11
const parent_section_11_canvas = document.querySelector('.section_11');

let executeTextSection_11 = false

window.addEventListener("scroll", function () {
    const box2Top = parent_section_11_canvas.getBoundingClientRect().top + parent_section_11_canvas.offsetHeight / 2;
    const bothSquares = this.document.querySelectorAll('.section_11 > div')
    const animateImages = this.document.querySelectorAll('.section_11 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_11 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_11) {
        bothSquares[0].classList.add('first_square')
        bothSquares[1].classList.add('second_square')
        animateImages[0].classList.add('img_wedding')
        animateImages[1].classList.add('section_11--recent_wedding')
        animateImages[2].classList.add('img_bg_stain_1')
        animateImages[3].classList.add('img_bg_stain_2')
        btnToAction.classList.add('section_11--btn_check')
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        animateImagesFlower[2].style.opacity = '1'
        animateImagesFlower[3].style.opacity = '1'
        parent_section_11_canvas.classList.add('section_resize')
        executeTextSection_11 = true
    }
});
const parent_section_12_canvas = document.querySelector('.section_12');


let executeTextSection_12 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_12_canvas.getBoundingClientRect().top + parent_section_12_canvas.offsetHeight / 2;
    const boxAnimate = this.document.querySelector('.section_12 > section')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_12) {
        boxAnimate.classList.add('section_12_animation')
        executeTextSection_12 = true
    }
});
const parent_section_13_canvas = document.querySelector('.section_13');

let executeTextSection_13 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_13_canvas.getBoundingClientRect().top + parent_section_13_canvas.offsetHeight / 2;
    const animateImages = this.document.querySelectorAll('.section_13 > img')
    const linesAnimate = this.document.querySelectorAll('.section_13 hr')
    const animateImagesFlower = this.document.querySelectorAll('.section_13 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0) {
        animateImages[0].classList.add('section_13--animate_img')
        animateImages[1].classList.add('img_bg_stain_1')
        animateImages[2].classList.add('img_bg_stain_2')
        linesAnimate[0].classList.add('lines_top_1');
        linesAnimate[1].classList.add('lines_top_2');
        linesAnimate[2].classList.add('lines_top_3');
        linesAnimate[3].classList.add('lines_top_4');
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        parent_section_13_canvas.classList.add('section_resize')
        executeTextSection_13 = true
    }
});

const parent_section_14_canvas = document.querySelector('.section_14');

let executeTextSection_14 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_14_canvas.getBoundingClientRect().top + parent_section_14_canvas.offsetHeight / 2;
    const animateCircles = this.document.querySelectorAll('.section_14 > div')
    const animateImages = this.document.querySelectorAll('.section_14 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_14 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0) {
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        animateImages[2].classList.add('section_14--img_invitation')
        animateCircles[0].classList.add('section_14-outer_circle')
        animateCircles[1].classList.add('section_14-inner_circle')
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        animateImagesFlower[2].style.opacity = '1'
        animateImagesFlower[3].style.opacity = '1'
        parent_section_14_canvas.classList.add('section_resize')
        executeTextSection_14 = true
    }
});

const fechaObjetivo = new Date(2024, 8, 28);

function tiempoRestante(fechaObjetivo) {
    let fechaActual = new Date();
    let tiempoActual = fechaActual.getTime();
    let tiempoObjetivo = fechaObjetivo.getTime();
    let diferencia = tiempoObjetivo - tiempoActual;

    // Si la diferencia es negativa o cero, mostrar 0 en todos los campos
    if (diferencia <= 0) {
        return {
            dias: 0,
            horas: 0,
            minutos: 0,
            segundos: 0
        };
    }

    let diasRestantes = Math.floor(diferencia / (1000 * 60 * 60 * 24));
    let horasRestantes = Math.floor((diferencia % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    let minutosRestantes = Math.floor((diferencia % (1000 * 60 * 60)) / (1000 * 60));
    let segundosRestantes = Math.floor((diferencia % (1000 * 60)) / 1000);

    return {
        dias: diasRestantes,
        horas: horasRestantes,
        minutos: minutosRestantes,
        segundos: segundosRestantes
    };
}

// Resto del código para actualizar el contador en el HTML sigue igual
function updateCounter() {
    let currentTime = tiempoRestante(fechaObjetivo);
    let divs = document.querySelectorAll('.section_1--background_circle');

    divs[0].innerHTML = `<p style="width: fit-content; height: 18px;font-size: 14px;display:flex;justify-content: center;align-items: center;margin-top:10px;">${currentTime.dias}</p>` + `<p style="width: fit-content; height: 12px;font-size: 14px;display:flex;justify-content: center;align-items: center;padding-bottom: 5px;">dias</p>`; // Días
    divs[1].innerHTML = `<p style="width: fit-content; height: 18px;font-size: 14px;display:flex;justify-content: center;align-items: center;margin-top:10px;">${currentTime.horas}</p>` + `<p style="width: fit-content; height: 12px;font-size: 14px;display:flex;justify-content: center;align-items: center;padding-bottom: 5px;">horas</p>`; // Horas
    divs[2].innerHTML = `<p style="width: fit-content; height: 18px;font-size: 14px;display:flex;justify-content: center;align-items: center;margin-top:10px;">${currentTime.minutos}</p>` + `<p style="width: fit-content; height: 12px;font-size: 14px;display:flex;justify-content: center;align-items: center;padding-bottom: 5px;">min</p>`; // Minutos
    divs[3].innerHTML = `<p style="width: fit-content; height: 18px;font-size: 14px;display:flex;justify-content: center;align-items: center;margin-top:10px;">${currentTime.segundos}</p>` + `<p style="width: fit-content; height: 12px;font-size: 14px;display:flex;justify-content: center;align-items: center;padding-bottom: 5px;">seg</p>`; // Segundos
}

// Actualizar el contador cada segundo
setInterval(updateCounter, 1000);

// Llamar a actualizarContador() una vez para mostrar el tiempo inicial
updateCounter();

const buttons = document.querySelectorAll("[data-carousel-button]")

buttons.forEach(button => {
    button.addEventListener("click", () => {
        const offset = button.dataset.carouselButton === "next" ? 1 : -1
        const slides = button
            .closest("[data-carousel]")
            .querySelector("[data-slides]")

        const activeSlide = slides.querySelector("[data-active]")
        let newIndex = [...slides.children].indexOf(activeSlide) + offset
        if (newIndex < 0) newIndex = slides.children.length - 1
        if (newIndex >= slides.children.length) newIndex = 0

        slides.children[newIndex].dataset.active = true
        delete activeSlide.dataset.active
    })
})


