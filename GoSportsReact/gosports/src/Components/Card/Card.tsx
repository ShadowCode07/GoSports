import React from 'react'
import "./Card.css"

type Props = {}

const Card = (props: Props) => {
  return (
    <div className="card">
        <img src='https://marketplace.canva.com/EAFxdcos7WU/1/0/1600w/canva-dark-blue-and-brown-illustrative-fitness-gym-logo-oqe3ybeEcQQ.jpg'/>

        <div className="details">
            <h2>Gym</h2>
            <p>Your favorite local gym</p>
        </div>
        <p className="Info">Lorem ipsum dolor sit amet consectetur, adipisicing elit. Optio, officia?</p>
    </div>
  )
}

export default Card