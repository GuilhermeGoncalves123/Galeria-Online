import "./Card.css"
import imgCard from '../../assets/img/neymendigo.jpg'

import imgPen from '../../assets/img/pen.svg'
import imgTrash from '../../assets/img/trash.svg'

export const Card = ({tituloCard}) => {
    return (
        <>
        <div className="cardImagem">
            <p>{tituloCard}</p>
            <img className="imgDoCard" src={imgCard} alt="Imagem relacionada ao Card"/>
            <div className="icons">   
                <img src={imgPen} alt="Icone de caneta para realizar uma alteração"/>
                 <img src={imgTrash} alt="Icone de uma lixeira para realizar a exclusao"/>
            </div>
        </div>
        </>
    )
}