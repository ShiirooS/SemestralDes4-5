async function Actividades() {
    try {
        
        const response =await fetch("http://localhost:5041/api/Activid")
        if(!response.ok){
            throw new Error("No pudo mostrar la informacion");
        }
        const data = await  response.json();
        const imagenActividad = data.imagenUrl;
        const imgElement = document.getElementById("imagenActividad");
        imgElement.src = imagenActividad;
    } catch (error) {
        console.error(error);
    }
}