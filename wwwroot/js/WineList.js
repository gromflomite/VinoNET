let imagenes = document.getElementsByClassName('imageCollapse')
let lista = document.getElementsByClassName('listaCollapse')

for (let i = 0; i < imagenes.length; i++) {
    imagenes[i].addEventListener('click', function () {

        if (lista[i].classList.contains('collapse')) {
            lista[i].classList.remove('collapse')//este hace lo mismo que el de abjo pero no lo elimina del array
        }
        else {
            lista[i].classList.add('collapse')
        }
        //lista[i].removeAttribute('class', 'collapse')//con removeAttribute lo elimina del todo 

    })
}