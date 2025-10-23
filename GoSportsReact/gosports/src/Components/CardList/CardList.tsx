import React from 'react'
import Card from '../Card/Card'

interface Props {}

const CardList = (props: Props) => {
  return (
    <div>
        <Card LocationName='Gym' Describtion='ABCD' CurrentLobbyCount={1} MaxLobbyCount={5}/>
        <Card LocationName='Gym' Describtion='EFGH' CurrentLobbyCount={2} MaxLobbyCount={6}/>
        <Card LocationName='Gym' Describtion='JKLM' CurrentLobbyCount={5} MaxLobbyCount={5}/>
    </div>
  )
}

export default CardList