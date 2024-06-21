const parentTagSection_1 = document.querySelector('.section_1')
const canvas = document.getElementById('logoCanvas');
const ctx = canvas.getContext('2d');


const containerWidth = document.getElementById('container_canvas').clientWidth;
const deviceHeight = window.innerHeight
canvas.width = Math.floor(containerWidth > 420 ? 420 : containerWidth);
canvas.height = Math.floor(deviceHeight);

let angleLeaveCircles = 0;

let angle = 0;  // Ángulo inicial de rotación

/*Seccion 2, otro canvas aparte */
const parent_section_2_canvas = document.querySelector('.section_2');
const canvas_section_2 = document.getElementById('section_canvas_2');
const section_2_ctx = canvas_section_2.getContext("2d");

const get_width_section_2 = parent_section_2_canvas.clientWidth;
canvas_section_2.width = Math.floor(get_width_section_2 > 420 ? 420 : get_width_section_2);
canvas_section_2.height = Math.floor(deviceHeight);

const centerX = Math.floor(canvas_section_2.width / 2);
const centerY = Math.floor(canvas_section_2.height / 2);


let angleLeave = 0;
function animate_1() {
    section_2_ctx.clearRect(0, 0, canvas_section_2.width, canvas_section_2.height);

    // Primera Linea
    section_2_ctx.strokeStyle = '#9b5a01';
    section_2_ctx.lineWidth = 2;
    section_2_ctx.save();
    section_2_ctx.translate(0, 80);
    section_2_ctx.rotate(-10 * Math.PI / 180);
    section_2_ctx.beginPath();
    section_2_ctx.moveTo(0, 0);
    section_2_ctx.lineTo(650, 0);
    section_2_ctx.stroke();
    section_2_ctx.restore();

    // Segunda línea
    section_2_ctx.strokeStyle = '#9b5a01';
    section_2_ctx.lineWidth = 2;
    section_2_ctx.save();
    section_2_ctx.translate(0, 0);
    section_2_ctx.rotate(18 * Math.PI / 180);
    section_2_ctx.beginPath();
    section_2_ctx.moveTo(0, 0);
    section_2_ctx.lineTo(650, 0);
    section_2_ctx.stroke();
    section_2_ctx.restore();
    // Tercera linea
    section_2_ctx.strokeStyle = '#9b5a01';
    section_2_ctx.lineWidth = 2;
    section_2_ctx.save();
    section_2_ctx.translate(0, centerY * 2);
    section_2_ctx.rotate(-13 * Math.PI / 180);
    section_2_ctx.beginPath();
    section_2_ctx.moveTo(0, 0);
    section_2_ctx.lineTo(650, 0);
    section_2_ctx.stroke();
    section_2_ctx.restore();
    //Cuarta Linea
    section_2_ctx.strokeStyle = '#9b5a01';
    section_2_ctx.lineWidth = 2;
    section_2_ctx.save();
    section_2_ctx.translate(0, centerY * 2 - 140);
    section_2_ctx.rotate(18 * Math.PI / 180);
    section_2_ctx.beginPath();
    section_2_ctx.moveTo(0, 0);
    section_2_ctx.lineTo(650, 0);
    section_2_ctx.stroke();
    section_2_ctx.restore();

    angleLeave += 0.1; // Incrementar el ángulo para la animación

    requestAnimationFrame(animate_1);
}
animate_1()

let executeTextSection_2 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_2_canvas.getBoundingClientRect().top + parent_section_2_canvas.offsetHeight / 2;
    const bothCircles = this.document.querySelectorAll('.section_2 > div')
    const allImageAnimate = this.document.querySelectorAll('.section_2 > img')
    const allImageAnimateFlower = this.document.querySelectorAll('.section_2 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_2) {
        canvas_section_2.classList.add('scale_section_2');
        allImageAnimate[0].classList.add('section_2_text')
        bothCircles[0].classList.add('section_2-outer_circle')
        bothCircles[1].classList.add('section_2-inner_circle')
        allImageAnimate[1].classList.add('img_bg_stain_1')
        allImageAnimate[2].classList.add('img_bg_stain_2')
        allImageAnimateFlower[0].style.opacity = '1';
        allImageAnimateFlower[1].style.opacity = '1';
        allImageAnimateFlower[2].style.opacity = '1';
        allImageAnimateFlower[3].style.opacity = '1';
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
const canvas_section_4 = document.getElementById('section_canvas_4');
const section_4_ctx = canvas_section_4.getContext('2d');


const get_width_section_4 = parent_section_4_canvas.offsetWidth;
const get_height_section_4 = parent_section_4_canvas.offsetHeight;

canvas_section_4.width = get_width_section_4 > 420 ? 420 : get_width_section_4;
canvas_section_4.height = deviceHeight


function animate_section_4() {
    section_4_ctx.clearRect(0, 0, canvas_section_4.width, canvas_section_4.height);

    section_4_ctx.strokeStyle = '#9b5a01';
    section_4_ctx.lineWidth = 2;
    section_4_ctx.save();
    section_4_ctx.translate(0, 80);
    section_4_ctx.rotate(-10 * Math.PI / 180);
    section_4_ctx.beginPath();
    section_4_ctx.moveTo(0, 0);
    section_4_ctx.lineTo(650, 0);
    section_4_ctx.stroke();
    section_4_ctx.restore();

    // Segunda línea
    section_4_ctx.strokeStyle = '#9b5a01';
    section_4_ctx.lineWidth = 2;
    section_4_ctx.save();
    section_4_ctx.translate(0, 0);
    section_4_ctx.rotate(18 * Math.PI / 180);
    section_4_ctx.beginPath();
    section_4_ctx.moveTo(0, 0);
    section_4_ctx.lineTo(650, 0);
    section_4_ctx.stroke();
    section_4_ctx.restore();
    // Tercera linea
    section_4_ctx.strokeStyle = '#9b5a01';
    section_4_ctx.lineWidth = 2;
    section_4_ctx.save();
    section_4_ctx.translate(0, centerY * 2);
    section_4_ctx.rotate(-13 * Math.PI / 180);
    section_4_ctx.beginPath();
    section_4_ctx.moveTo(0, 0);
    section_4_ctx.lineTo(650, 0);
    section_4_ctx.stroke();
    section_4_ctx.restore();
    //Cuarta Linea
    section_4_ctx.strokeStyle = '#9b5a01';
    section_4_ctx.lineWidth = 2;
    section_4_ctx.save();
    section_4_ctx.translate(0, centerY * 2 - 140);
    section_4_ctx.rotate(18 * Math.PI / 180);
    section_4_ctx.beginPath();
    section_4_ctx.moveTo(0, 0);
    section_4_ctx.lineTo(650, 0);
    section_4_ctx.stroke();
    section_4_ctx.restore();




    angleLeave += 1; // Incrementar el ángulo para la  animación

    requestAnimationFrame(animate_section_4);
}
animate_section_4();
let executeTextSection_4 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_4_canvas.getBoundingClientRect().top + parent_section_4_canvas.offsetHeight / 2;
    const textAnimate = document.querySelector('.section_4 > p')
    const bothCircles = this.document.querySelectorAll('.section_4 > span')
    const canvasVisibility = this.document.querySelector('.section_4 > canvas')
    const animateImages = this.document.querySelectorAll('.section_4 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_4 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_4) {
        bothCircles[0].classList.add('section_4-outer_circle')
        bothCircles[1].classList.add('section_4-inner_circle')
        textAnimate.classList.add('phrase_bible')
        canvasVisibility.classList.add('scale_section_4')
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        animateImagesFlower[0].style.opacity = '1';
        animateImagesFlower[1].style.opacity = '1';
        animateImagesFlower[2].style.opacity = '1';
        animateImagesFlower[3].style.opacity = '1';
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
let angleRotationSection5 = 5;

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
        animateImagesFlower[2].style.opacity = '1';
        animateImagesFlower[3].style.opacity = '1';
        executeTextSection_5 = true
    }
});

// Seccion 6
const parent_section_6_canvas = document.querySelector('.section_6');
const canvas_section_6 = document.getElementById('section_canvas_6');
const section_6_ctx = canvas_section_6.getContext('2d');


const get_width_section_6 = parent_section_6_canvas.offsetWidth;


canvas_section_6.width = get_width_section_6 > 420 ? 420 : get_width_section_6;
canvas_section_6.height = deviceHeight;


function animate_section_6() {
    section_6_ctx.clearRect(0, 0, canvas_section_6.width, canvas_section_6.height);

    // Primera Linea
    section_6_ctx.strokeStyle = '#9b5a01';
    section_6_ctx.lineWidth = 2;
    section_6_ctx.save();
    section_6_ctx.translate(0, 80);
    section_6_ctx.rotate(-10 * Math.PI / 180);
    section_6_ctx.beginPath();
    section_6_ctx.moveTo(0, 0);
    section_6_ctx.lineTo(650, 0);
    section_6_ctx.stroke();
    section_6_ctx.restore();

    // Segunda línea
    section_6_ctx.strokeStyle = '#9b5a01';
    section_6_ctx.lineWidth = 2;
    section_6_ctx.save();
    section_6_ctx.translate(0, 0);
    section_6_ctx.rotate(18 * Math.PI / 180);
    section_6_ctx.beginPath();
    section_6_ctx.moveTo(0, 0);
    section_6_ctx.lineTo(650, 0);
    section_6_ctx.stroke();
    section_6_ctx.restore();
    // Tercera linea
    section_6_ctx.strokeStyle = '#9b5a01';
    section_6_ctx.lineWidth = 2;
    section_6_ctx.save();
    section_6_ctx.translate(0, centerY * 2);
    section_6_ctx.rotate(-13 * Math.PI / 180);
    section_6_ctx.beginPath();
    section_6_ctx.moveTo(0, 0);
    section_6_ctx.lineTo(650, 0);
    section_6_ctx.stroke();
    section_6_ctx.restore();
    //Cuarta Linea
    section_6_ctx.strokeStyle = '#9b5a01';
    section_6_ctx.lineWidth = 2;
    section_6_ctx.save();
    section_6_ctx.translate(0, centerY * 2 - 140);
    section_6_ctx.rotate(18 * Math.PI / 180);
    section_6_ctx.beginPath();
    section_6_ctx.moveTo(0, 0);
    section_6_ctx.lineTo(650, 0);
    section_6_ctx.stroke();
    section_6_ctx.restore();
    //Rosa Esquina

    angleLeave += 1;

    requestAnimationFrame(animate_section_6);


}
animate_section_6()


let executeTextSection_6 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_6_canvas.getBoundingClientRect().top + parent_section_6_canvas.offsetHeight / 2;
    const animateCanvas = this.document.querySelector('.section_6 > canvas')
    const imageAnimate = this.document.querySelectorAll('.section_6 > img')
    const imageAnimateFlower = this.document.querySelectorAll('.section_6 > aside')
    const imageInvitationAnimate = this.document.querySelector('.section_6 > section > img')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_6) {
        imageAnimate[0].classList.add('img_bg_stain_1')
        imageAnimate[1].classList.add('img_bg_stain_2')
        animateCanvas.classList.add('scale_section_6')
        imageInvitationAnimate.classList.add('scale_section_6_img')
        imageAnimateFlower[0].style.opacity = '1';
        imageAnimateFlower[1].style.opacity = '1';
        imageAnimateFlower[2].style.opacity = '1';
        imageAnimateFlower[3].style.opacity = '1';
        executeTextSection_6 = true
    }
});
// Seccion 7
const parent_section_7_canvas = document.querySelector('.section_7');
const canvas_section_7 = document.getElementById('section_canvas_7');
const section_7_ctx = canvas_section_7.getContext('2d');


const get_width_section_7 = parent_section_7_canvas.offsetWidth;
const get_height_section_7 = parent_section_7_canvas.offsetHeight;

canvas_section_7.width = get_width_section_7 > 420 ? 420 : get_width_section_7;
canvas_section_7.height = deviceHeight;

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
        executeTextSection_7 = true
    }
});

//Seccion 8
const parent_section_8_canvas = document.querySelector('.section_8');
const canvas_section_8 = document.getElementById('section_canvas_8');
const section_8_ctx = canvas_section_8.getContext('2d');


const get_width_section_8 = parent_section_8_canvas.offsetWidth;


canvas_section_8.width = get_width_section_8 > 420 ? 420 : get_width_section_8;
canvas_section_8.height = deviceHeight;

function animate_section_8() {
    section_8_ctx.clearRect(0, 0, canvas_section_8.width, canvas_section_8.height);
    section_8_ctx.strokeStyle = '#9b5a01';
    section_8_ctx.lineWidth = 2;
    section_8_ctx.save();
    section_8_ctx.translate(0, 80);
    section_8_ctx.rotate(-10 * Math.PI / 180);
    section_8_ctx.beginPath();
    section_8_ctx.moveTo(0, 0);
    section_8_ctx.lineTo(650, 0);
    section_8_ctx.stroke();
    section_8_ctx.restore();

    // Segunda línea
    section_8_ctx.strokeStyle = '#9b5a01';
    section_8_ctx.lineWidth = 2;
    section_8_ctx.save();
    section_8_ctx.translate(0, 0);
    section_8_ctx.rotate(18 * Math.PI / 180);
    section_8_ctx.beginPath();
    section_8_ctx.moveTo(0, 0);
    section_8_ctx.lineTo(650, 0);
    section_8_ctx.stroke();
    section_8_ctx.restore();
    // Tercera linea
    section_8_ctx.strokeStyle = '#9b5a01';
    section_8_ctx.lineWidth = 2;
    section_8_ctx.save();
    section_8_ctx.translate(0, centerY * 2);
    section_8_ctx.rotate(-13 * Math.PI / 180);
    section_8_ctx.beginPath();
    section_8_ctx.moveTo(0, 0);
    section_8_ctx.lineTo(650, 0);
    section_8_ctx.stroke();
    section_8_ctx.restore();
    //Cuarta Linea
    section_8_ctx.strokeStyle = '#9b5a01';
    section_8_ctx.lineWidth = 2;
    section_8_ctx.save();
    section_8_ctx.translate(0, centerY * 2 - 140);
    section_8_ctx.rotate(18 * Math.PI / 180);
    section_8_ctx.beginPath();
    section_8_ctx.moveTo(0, 0);
    section_8_ctx.lineTo(650, 0);
    section_8_ctx.stroke();
    section_8_ctx.restore();

    requestAnimationFrame(animate_section_8)
}
animate_section_8()
let executeTextSection_8 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_8_canvas.getBoundingClientRect().top + parent_section_8_canvas.offsetHeight / 2;
    const animateSection = this.document.querySelector('.section_8 > section')
    const animateCanvas = this.document.querySelector('.section_8 > canvas')
    const animateImages = this.document.querySelectorAll('.section_8 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_8 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0) {
        animateSection.classList.add('scale_section_8')
        animateCanvas.classList.add('scale_section_8_canvas')
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        executeTextSection_8 = true
    }
});
//Seccion 9
const parent_section_9_canvas = document.querySelector('.section_9');
const canvas_section_9 = document.getElementById('section_canvas_9');

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
    const animateCanvas = this.document.querySelector('.section_9 > canvas')
    const animateImages = this.document.querySelectorAll('.section_9 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_9 > aside')
    const centerImage = this.document.querySelector('.section_9--description > img')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_9) {
        animateCanvas.classList.add('animate_section_9_canvas')
        animateSection.classList.add('animate_section_9')
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        btnChecks[0].classList.add('section_9--btn_check')
        btnChecks[1].classList.add('section_9--btn_check')
        centerImage.classList.add('scale_section_6_img')
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        executeTextSection_9 = true
    }
});
//Seccion 10
const parent_section_10_canvas = document.querySelector('.section_10');
const canvas_section_10 = document.getElementById('section_canvas_10');
const section_10_ctx = canvas_section_10.getContext('2d');


const get_width_section_10 = parent_section_10_canvas.offsetWidth;
const get_height_section_10 = parent_section_10_canvas.offsetHeight;

canvas_section_10.width = get_width_section_10 > 420 ? 420 : get_width_section_10;
canvas_section_10.height = deviceHeight;
function animate_section_10() {
    section_10_ctx.clearRect(0, 0, canvas_section_10.width, canvas_section_10.height);
    // Primera Linea
    section_10_ctx.strokeStyle = '#9b5a01';
    section_10_ctx.lineWidth = 2;
    section_10_ctx.save();
    section_10_ctx.translate(0, 80);
    section_10_ctx.rotate(-10 * Math.PI / 180);
    section_10_ctx.beginPath();
    section_10_ctx.moveTo(0, 0);
    section_10_ctx.lineTo(650, 0);
    section_10_ctx.stroke();
    section_10_ctx.restore();

    // Segunda línea
    section_10_ctx.strokeStyle = '#9b5a01';
    section_10_ctx.lineWidth = 2;
    section_10_ctx.save();
    section_10_ctx.translate(0, 0);
    section_10_ctx.rotate(18 * Math.PI / 180);
    section_10_ctx.beginPath();
    section_10_ctx.moveTo(0, 0);
    section_10_ctx.lineTo(650, 0);
    section_10_ctx.stroke();
    section_10_ctx.restore();
    // Tercera linea
    section_10_ctx.strokeStyle = '#9b5a01';
    section_10_ctx.lineWidth = 2;
    section_10_ctx.save();
    section_10_ctx.translate(0, centerY * 2);
    section_10_ctx.rotate(-13 * Math.PI / 180);
    section_10_ctx.beginPath();
    section_10_ctx.moveTo(0, 0);
    section_10_ctx.lineTo(650, 0);
    section_10_ctx.stroke();
    section_10_ctx.restore();
    //Cuarta Linea
    section_10_ctx.strokeStyle = '#9b5a01';
    section_10_ctx.lineWidth = 2;
    section_10_ctx.save();
    section_10_ctx.translate(0, centerY * 2 - 140);
    section_10_ctx.rotate(18 * Math.PI / 180);
    section_10_ctx.beginPath();
    section_10_ctx.moveTo(0, 0);
    section_10_ctx.lineTo(650, 0);
    section_10_ctx.stroke();
    section_10_ctx.restore();






    requestAnimationFrame(animate_section_10)
}
animate_section_10()
let executeTextSection_10 = false
const btnAction = document.querySelector('.section_10 > main > button')
btnAction.addEventListener('click', () => {
    window.open('gift_inv.pdf', '_blank')
})
window.addEventListener("scroll", function () {
    const box2Top = parent_section_10_canvas.getBoundingClientRect().top + parent_section_10_canvas.offsetHeight / 2;
    const animateCanvas = this.document.querySelector('.section_10 > canvas')
    const animateImages = this.document.querySelectorAll('.section_10 > img')
    const bothCircles = this.document.querySelectorAll('.section_10 > div')
    const textAnimate = this.document.querySelector('.section_10 > main')
    const animateImagesFlower = this.document.querySelectorAll('.section_10 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_10) {
        animateCanvas.classList.add('animate_section_9_canvas')
        animateImages[0].classList.add('img_bg_stain_1')
        animateImages[1].classList.add('img_bg_stain_2')
        bothCircles[0].classList.add('section_10-outer_circle')
        bothCircles[1].classList.add('section_10-inner_circle')
        textAnimate.classList.add('phrase_section_10')
        btnAction.classList.add('phrase_section_10--btn_check')
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
        executeTextSection_10 = true
    }
});
//Seccion 11
const parent_section_11_canvas = document.querySelector('.section_11');
const canvas_section_11 = document.getElementById('section_canvas_11');

let executeTextSection_11 = false
const btnToAction = document.querySelector('.section_11 > button')
btnToAction.addEventListener('click', () => {
    window.open('gift_inv.pdf', '_blank')
})
window.addEventListener("scroll", function () {
    const box2Top = parent_section_11_canvas.getBoundingClientRect().top + parent_section_11_canvas.offsetHeight / 2;
    const bothSquares = this.document.querySelectorAll('.section_11 > div')
    const animateCanvas = this.document.querySelector('.section_11 > canvas')
    const animateImages = this.document.querySelectorAll('.section_11 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_11 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0 && !executeTextSection_11) {
        animateCanvas.classList.add('scale_section_2');
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
const canvas_section_13 = document.getElementById('section_canvas_13');
const section_13_ctx = canvas_section_13.getContext('2d');


const get_width_section_13 = parent_section_13_canvas.offsetWidth;


canvas_section_13.width = get_width_section_13 > 420 ? 420 : get_width_section_13;
canvas_section_13.height = deviceHeight;

function animate_section_13() {
    section_13_ctx.clearRect(0, 0, canvas_section_13.width, canvas_section_13.height);
    // Primera Linea
    section_13_ctx.strokeStyle = '#9b5a01';
    section_13_ctx.lineWidth = 2;
    section_13_ctx.save();
    section_13_ctx.translate(0, 80);
    section_13_ctx.rotate(-10 * Math.PI / 180);
    section_13_ctx.beginPath();
    section_13_ctx.moveTo(0, 0);
    section_13_ctx.lineTo(650, 0);
    section_13_ctx.stroke();
    section_13_ctx.restore();

    // Segunda línea
    section_13_ctx.strokeStyle = '#9b5a01';
    section_13_ctx.lineWidth = 2;
    section_13_ctx.save();
    section_13_ctx.translate(0, 0);
    section_13_ctx.rotate(18 * Math.PI / 180);
    section_13_ctx.beginPath();
    section_13_ctx.moveTo(0, 0);
    section_13_ctx.lineTo(650, 0);
    section_13_ctx.stroke();
    section_13_ctx.restore();
    // Tercera linea
    section_13_ctx.strokeStyle = '#9b5a01';
    section_13_ctx.lineWidth = 2;
    section_13_ctx.save();
    section_13_ctx.translate(0, centerY * 2);
    section_13_ctx.rotate(-13 * Math.PI / 180);
    section_13_ctx.beginPath();
    section_13_ctx.moveTo(0, 0);
    section_13_ctx.lineTo(650, 0);
    section_13_ctx.stroke();
    section_13_ctx.restore();
    //Cuarta Linea
    section_13_ctx.strokeStyle = '#9b5a01';
    section_13_ctx.lineWidth = 2;
    section_13_ctx.save();
    section_13_ctx.translate(0, centerY * 2 - 140);
    section_13_ctx.rotate(18 * Math.PI / 180);
    section_13_ctx.beginPath();
    section_13_ctx.moveTo(0, 0);
    section_13_ctx.lineTo(650, 0);
    section_13_ctx.stroke();
    section_13_ctx.restore();

    requestAnimationFrame(animate_section_13)
}
animate_section_13()
let executeTextSection_13 = false
window.addEventListener("scroll", function () {
    const box2Top = parent_section_13_canvas.getBoundingClientRect().top + parent_section_13_canvas.offsetHeight / 2;
    const animateCanvas = this.document.querySelector('.section_13 > canvas')
    const animateImages = this.document.querySelectorAll('.section_13 > img')
    const animateImagesFlower = this.document.querySelectorAll('.section_13 > aside')
    if (box2Top <= window.innerHeight && box2Top >= 0) {
        animateImages[0].classList.add('section_13--animate_img')
        animateImages[1].classList.add('img_bg_stain_1')
        animateImages[2].classList.add('img_bg_stain_2')
        animateCanvas.classList.add('section_13--canvas_animate')
        animateImagesFlower[0].style.opacity = '1'
        animateImagesFlower[1].style.opacity = '1'
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
        executeTextSection_14 = true
    }
});

const fechaObjetivo = new Date(2024, 5, 28);

function tiempoRestante(fechaObjetivo) {
    let fechaActual = new Date();
    let tiempoActual = fechaActual.getTime();
    let tiempoObjetivo = fechaObjetivo.getTime();
    let diferencia = tiempoObjetivo - tiempoActual;

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


// Función para actualizar los divs con el tiempo restante
function updateCounter() {
    let currentTime = tiempoRestante(fechaObjetivo);
    let divs = document.querySelectorAll('.section_1--background_circle');

    divs[0].innerHTML = `<p  style="width: fit-content; height: 18px;font-size: 14px;display:flex;justify-content: center;align-items: center;margin-top:10px;">${currentTime.dias}</p>` + `<p style="width: fit-content; height: 12px;font-size: 14px;display:flex;justify-content: center;align-items: center;">dias</p>`;//Dias
    divs[1].innerHTML = `<p  style="width: fit-content; height: 18px;font-size: 14px;display:flex;justify-content: center;align-items: center;margin-top:10px;">${currentTime.horas}</p>` + `<p style="width: fit-content; height: 12px;font-size: 14px;display:flex;justify-content: center;align-items: center;">horas</p>`;//Horas
    divs[2].innerHTML = `<p  style="width: fit-content; height: 18px;font-size: 14px;display:flex;justify-content: center;align-items: center;margin-top:10px;">${currentTime.minutos}</p>` + `<p style="width: fit-content; height: 12px;font-size: 14px;display:flex;justify-content: center;align-items: center;">min</p>`; //Minutos
    divs[3].innerHTML = `<p  style="width: fit-content; height: 18px;font-size: 14px;display:flex;justify-content: center;align-items: center;margin-top:10px;">${currentTime.segundos}</p>` + `<p style="width: fit-content; height: 12px;font-size: 14px;display:flex;justify-content: center;align-items: center;">seg</p>`;//Segundos
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

