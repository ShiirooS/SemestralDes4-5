let categoriaSeleccionada = null; 

async function obtenerCategorias() {
    try {
        const response = await fetch("http://localhost:5041/api/Categorias");
        if (!response.ok) {
            throw new Error("No se pudo obtener las categorías");
        }
         const categorias = await response.json();
         const categoriasContenedor = document.getElementById("categorias");

         categoriasContenedor.innerHTML = '';

             categorias.forEach(categoria => {
             const categoriaElemento = document.createElement("div");
             categoriaElemento.classList.add("categoria");
             categoriaElemento.onclick = (event) => {
                event.stopPropagation(); 
                verSubcategorias(categoria.idCategoria);
            };

               categoriaElemento.innerHTML = `
                <img src="/IMG/categorias/${categoria.nombre.toLowerCase().replace(/\s/g, '-')}.jpg" alt="${categoria.nombre}">
                <p>${categoria.nombre}</p>
            `;

            categoriasContenedor.appendChild(categoriaElemento);
        });
    } catch (error) {
        console.error(error);
    }
}

async function verSubcategorias(idCategoria) {
    const subcategoriasContenedor = document.getElementById("subcategorias-lista");

    if (categoriaSeleccionada === idCategoria) {
        subcategoriasContenedor.innerHTML = '';
        document.getElementById("subcategorias").style.display = "none";
        categoriaSeleccionada = null; 
        return;
    }

    try {
        const response = await fetch(`http://localhost:5041/api/Categorias/${idCategoria}/subcategorias`);
          if (!response.ok) {
            throw new Error(`No se encontraron subcategorías para la categoría con ID ${idCategoria}`);
        }
        const subcategorias = await response.json();

        subcategoriasContenedor.innerHTML = '';

        if (subcategorias.length === 0) {
            subcategoriasContenedor.innerHTML = "<p>No hay subcategorías disponibles.</p>";
        } else {
            subcategorias.forEach(subcategoria => {
                  const subcategoriaElemento = document.createElement("div");
                   subcategoriaElemento.classList.add("subcategoria");
                   subcategoriaElemento.onclick = (event) => {
                    event.stopPropagation(); 
                    irASubcategoria(subcategoria.idSubcategoria);
                };

                    subcategoriaElemento.innerHTML = `
                    <img src="/IMG/subcategorias/${subcategoria.nombre.toLowerCase().replace(/\s/g, '-')}.jpg" alt="${subcategoria.nombre}">
                    <p>${subcategoria.nombre}</p>
                `;

                subcategoriasContenedor.appendChild(subcategoriaElemento);
            });
        }

        document.getElementById("subcategorias").style.display = "block";
        categoriaSeleccionada = idCategoria; 
    } catch (error) {
        console.error(error);
    }
}

function irASubcategoria(idSubcategoria) {
    window.location.href = `subcategoria.html?id=${idSubcategoria}`;
    mostrarActividadesPorSubcategoria(idSubcategoria);
}


document.addEventListener("click", (event) => {
    const subcategoriasContenedor = document.getElementById("subcategorias");
    if (categoriaSeleccionada && !subcategoriasContenedor.contains(event.target)) {
        subcategoriasContenedor.style.display = "none";
        categoriaSeleccionada = null; 
    }
});

obtenerCategorias();


