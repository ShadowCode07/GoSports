import React from 'react'
import "./Card.css"

interface Props {
    LocationName: string,
    Describtion: string
    CurrentLobbyCount: number,
    MaxLobbyCount: number
}

const Card = ({LocationName, Describtion, CurrentLobbyCount, MaxLobbyCount}: Props) => {
  return (
    <div className="card">
        <img src='https://marketplace.canva.com/EAFxdcos7WU/1/0/1600w/canva-dark-blue-and-brown-illustrative-fitness-gym-logo-oqe3ybeEcQQ.jpg'/>

        <div className="details">
            <h2>{LocationName}</h2>
            <p>{Describtion}</p>
        </div>
        <p className="Info">Lorem ipsum dolor sit amet consectetur, adipisicing elit. Optio, officia?</p>
        <p>Current lobby count: {CurrentLobbyCount}</p>
        <p>Max lobby count: {MaxLobbyCount}</p>
    </div>
  )
}

export default Card