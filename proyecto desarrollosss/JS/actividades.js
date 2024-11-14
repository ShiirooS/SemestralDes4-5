async function Actividades() {
    try {
        const response = await fetch("http://localhost:5041/api/Activid");
        if (!response.ok) {
            throw new Error("No pudo mostrar la información");
        }
        const actividades = await response.json();
        const actividadesContenedor = document.getElementById("actividades");
        actividadesContenedor.innerHTML = ''; 

        actividades.forEach(element => {
            const actividadesElemento = document.createElement("div");
            actividadesElemento.classList.add("actividad");

            actividadesElemento.innerHTML = `
                <img src="${element.imagenUrl}" alt="${element.nombre}" class="actividad-imagen">
                <div class="contenido">
                    <h3>${element.nombre}</h3>
                    <p>Cupos disponibles: ${element.limiteCupos}</p>
                </div>
                <button class="boton-inscribir">Inscribirme</button>
            `;
            actividadesContenedor.appendChild(actividadesElemento);
        });
    } catch (error) {
        console.error(error);
    }
}
Actividades();