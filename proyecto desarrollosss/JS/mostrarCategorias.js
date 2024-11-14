let categoriaSeleccionada = null; 
let subCategoriaSeleccionada = null;

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
            
                <p>${categoria.nombre}</p>
            `;

            categoriasContenedor.appendChild(categoriaElemento);
            });
        } catch (error) {
        console.error(error);
    }
}
// ------------------------------------------------------------------------------------------------------------------------------------
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
document.addEventListener("click", (event) => {
    const subcategoriasContenedor = document.getElementById("subcategorias");
    if (categoriaSeleccionada && !subcategoriasContenedor.contains(event.target)) {
        subcategoriasContenedor.style.display = "none";
        categoriaSeleccionada = null; 
    }
});

obtenerCategorias();

// ------------------------------------------------------------------------------------------------------------------------------------
async function mostrarActividadesPorSubcategoria(idSubcategoria) {
    try {
        const response = await fetch(`http://localhost:5041/api/Activid/subcategoria/${idSubcategoria}`);
        if (!response.ok) {
            throw new Error(`No se encontraron actividades para la subcategoría con ID ${idSubcategoria}`);
        }
        const actividades = await response.json();
        const actividadesContenedor = document.getElementById("actividades-lista");
        actividadesContenedor.innerHTML = '';

        if (actividades.length === 0) {
            actividadesContenedor.innerHTML = "<p>No hay actividades disponibles.</p>";
        } else {
            actividades.forEach(actividad => {
                const actividadElemento = document.createElement("div");
                actividadElemento.classList.add("actividad");
                actividadElemento.innerHTML = `
                    <h3>${actividad.nombre}</h3>
                    <p>${actividad.descripcion}</p>
                `;
                actividadesContenedor.appendChild(actividadElemento);
            });
        }

        document.getElementById("actividades").style.display = "block";
    } catch (error) {
        console.error(error);
    }
}
function irASubcategoria(idSubcategoria) {
    mostrarActividadesPorSubcategoria(idSubcategoria);
}

// ------------------------------------------------------------------------------------------------------------------------------------
// configuracion del scroll
document.getElementById("scroll-left").addEventListener("click", function() {
    document.getElementById("subcategorias-lista").scrollBy({ left: -200, behavior: 'smooth' });
});

document.getElementById("scroll-right").addEventListener("click", function() {
    document.getElementById("subcategorias-lista").scrollBy({ left: 200, behavior: 'smooth' });
});   

